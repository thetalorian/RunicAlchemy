using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{

    // The crystal is the main
    // object for storing and converting
    // mana.

    public bool active;
    public Rune rune;
    public bool isWell;
    public WorldCrystal myWorld;
    Image fillBar;
    Text magicMeter;
    private Crystal well;

    // Set CrystalInfo Settings
    public Sprite crystalSprite;


    // Set Draw Sources
    public Crystal[] sources;
    int packetSize;
    int totalPacketSize;

    public int crystalSize;
    private int growCost;
    public int crystalRefinement;
    private int refinementCost;

 
    // Current Magic level
    public int heldAmount;
    private int reserveAmount;
    public int capacity;
    // Speed settings
    private int tick;
    private int tickMax;
    private Queue waitlist;

	// Use this for initialization
	void Start () {
        // Set source amounts
        totalPacketSize = 20;

        if (sources.Length > 0)
        {
            packetSize = Mathf.FloorToInt(totalPacketSize / sources.Length);
        } else {
            packetSize = totalPacketSize;
        }
        heldAmount = 0;
        tick = 0;
        tickMax = 50;
        enabled = true;
        capacity = 20;
        crystalSize = 1;
        crystalRefinement = 1;
        growCost = crystalSize;
        refinementCost = crystalRefinement;
        well = GameObject.Find("Well").GetComponent<Crystal>();


        if (isWell)
        {
            magicMeter = transform.Find("MagicMeter/Counter").GetComponent<Text>();
        }
        else
        {
            fillBar = transform.Find("FillBar/Filling").GetComponent<Image>();
        }
        waitlist = new Queue();
	}
	
	// Update is called once per frame
	void Update () {
        tick += 1;
        if (tick >= tickMax)
        {
            tick = 0;
            convertMagic();
        }
	}

    public void addMagic(int amount){
        heldAmount += amount;
        if (heldAmount > capacity) {
            heldAmount = capacity;
        }
        updateBar();
    }

    private void convertMagic () {
        // The primary purpose of a crystal
        // is to convert magic from its sources.

        // The conversion operates by grabing 1 or more packets
        // of magic from each source, and if succesful, adding
        // the equivalent amount at this crystal.

        if (sources.Length > 0) {
            // If there are no sources, there's nothing to convert.
            if (heldAmount < capacity) {
                // If there's no room, don't bother.

                // Now we need to check the sources and determine
                // how many packets we can take.

                int packets = crystalRefinement;

                // Reduce requested packets to fit in free space.
                int freespace = capacity - heldAmount;
                if (freespace < packets) {
                    packets = freespace;
                }

                bool canConvert = true;
                foreach (Crystal source in sources) {
                    int available = source.hasAmount(packetSize, this);
                    if (available < packets) {
                        packets = available;
                    }
                }

                if (packets > 0) {
                    bool success = true;
                    foreach (Crystal source in sources) {
                        success = success && source.takeAmount(packetSize * packets, this);
                    }
                    if (success) {
                        addMagic(packets);
                        //myWorld.BringTheLightning();
                    }
                }
                
            }
        }
    }

    public int hasAmount(int packetSize, Component requester)
    {
        // Returns the number of times the requester
        // could potentially take the requested amount.


        // First: Determine if the requester is a crystal or
        // not.
        bool isCrystal = false;
        if (requester is Crystal) {
            isCrystal = true;
        }

        if (isCrystal) {
            // If the requester is a crystal, we need
            // to see where it is in the waitlist.
            //Debug.Log(this.ToString() + " receiving hasAmount request from " + requester.ToString());
            //Debug.Log("Current waitlist: " + waitlist.Count.ToString());
            if (waitlist.Count > 0)
            {
                //Debug.Log("First Item in waitlist: " + waitlist.Peek().ToString());
            }
            if ((waitlist.Count > 0) && (waitlist.Peek() == requester)) {
                // If it's first in line, come back with the available amount,
                // and reserve it.
                int availablePackets = Mathf.FloorToInt(heldAmount / packetSize);
                reserveAmount = availablePackets * packetSize;
                return availablePackets;
            } else {
                // If it's not first in line, we need to check the list,
                // and add it if it isn't already waiting.
                if (!waitlist.Contains(requester)) {
                    waitlist.Enqueue(requester);
                }
                return 0;
            }
            
        } else {
            // If the requester is not a crystal, bypass the
            // waitlist, but honor any reservations.
            if ((heldAmount - reserveAmount) > packetSize) {
                return 1;
            } else {
                return 0;
            }
        }

    }

    public void clearReserve(Component requester) {
        // Only the first item of the waitlist can set a reserve
        // so we want to ensure that the clear request is coming from
        // that requester.
        if ((waitlist.Count > 0) && (waitlist.Peek() == requester)) {
            reserveAmount = 0;
        }

    }

    public bool takeAmount(int amount, Component requester)
    {
        // If the requester is a crystal, it can only grab
        // if it is next on the waitlist.
        bool canTake = true;
        if ((requester is Crystal) && (waitlist.Peek() != requester)) {
            canTake = false;
        }

        if (canTake) {
            if (requester is Crystal) {
                reserveAmount = 0;
                waitlist.Dequeue();
            }
            if (heldAmount >= amount) {
                heldAmount -= amount;
                updateBar();
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }

    }

    private void updateBar(){
        if (isWell)
        {
            magicMeter.text = heldAmount.ToString();
        } else {
            float fillPercent = (heldAmount * 1f) / capacity;
            fillBar.fillAmount = fillPercent;
        }
    }


    // Upgrading
    // ---------
    // Upgrading crystals allows them to work more efficiently.
    // There are two types of upgrades: Refinement and Growth.

    // Growing a crystal allows it to hold more, increasing its capacity.
    // Crystal growth is indicated by a larger crystal icon on screen
    // and in the world, but size can exceed the art assets, so there is
    // no true maximum size.

    public void grow(){
        if (well.hasAmount(growCost, well) > 0) {
            well.takeAmount(growCost, well);
            crystalSize += 1;
            growCost = crystalSize;
            capacity = Mathf.FloorToInt(Mathf.Pow(10, crystalSize));
        }
    }

    // Refining a crystal improves its overall performance, allowing it to 
    // draw more at a time, making for faster conversion.

    public void refine(){
        if (well.hasAmount(refinementCost, well) > 0) {
            well.takeAmount(refinementCost, well);
            crystalRefinement += 1;
            refinementCost = crystalRefinement;
        }
    }
}
