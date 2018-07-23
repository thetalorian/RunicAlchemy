using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miCrystal : MagicItem {

    [Header("Crystal Attributes")]
    [SerializeField]
    private int heldAmount;
    private int reserveAmount;
    [SerializeField]
    private Rune rune;
    [SerializeField]
    private CrystalTypes crystalTypes;
    [SerializeField]
    private CrystalType crystalType;
    [SerializeField]
    private int capacity;

    public miCrystal[] sources;
    int packetSize;
    int totalPacketSize;
    public bool isWell = false;
    public UpgradableStat crystalSize;
    public UpgradableStat crystalRefinement;


    private Queue waitList;

    private int tick;
    private int tickMax;

	// Use this for initialization
	void Start () {
        totalPacketSize = 20;

        if (sources.Length > 0)
        {
            packetSize = Mathf.FloorToInt(totalPacketSize / sources.Length);
        }else {
            packetSize = totalPacketSize;
        }
        heldAmount = 0;
        tick = 0;
        tickMax = 50;
        capacity = 20;
        crystalSize = new UpgradableStat("crystalSize");
        if (isWell) {
            crystalSize.level = 0;
        }
        SetCrystalType();
        crystalRefinement = new UpgradableStat("crystalRefinement");
        if (!isWell)
        {
            upgradableStats.Add(crystalSize);
            upgradableStats.Add(crystalRefinement);
        }
        waitList = new Queue();
		
	}
	
	// Update is called once per frame
	void Update () {
        tick++;
        if (tick >= tickMax) {
            tick = 0;
            convertMagic();
        }
	}

    public override void StatUpgraded(UpgradableStat stat) {
        SetCrystalType();
    }

    private void SetCrystalType()   {
        crystalType = crystalTypes.GetTypeForSize(crystalSize.level);
        if (isWell)
        {
            capacity = -1;
        }
        else
        {
            capacity = Mathf.FloorToInt(2 * Mathf.Pow(10, crystalSize.level));
        }
    }

    private void convertMagic() {
        if (sources.Length > 0) {
            // How much can we take?
            int maxPackets = crystalRefinement.level;
            int freeSpacePackets = Mathf.FloorToInt((capacity - heldAmount) / packetSize) + 1;
            int packets = Mathf.Min(freeSpacePackets, maxPackets);

            if (packets > 0)
            {
                foreach (miCrystal source in sources)
                {
                    int available = source.hasAmount(packetSize, this);
                    packets = Mathf.Min(packets, available);
                }
            }

            if (packets > 0)
            {
                bool success = true;
                foreach (miCrystal source in sources) {
                    success = success && source.takeAmount(packetSize * packets, this);
                }
                if (success) {
                    addMagic(packets);
                }
            }

        }
    }

    public void addMagic(int amount) {
        heldAmount += amount;
        if ((heldAmount > capacity) && (capacity != -1))
        {
            heldAmount = capacity;
        }
    }

    public void SetRune(Rune newRune)
    {
        rune = newRune;
    }

    public int hasAmount(int packetSize, Component requester)
    {
        if (requester is miCrystal) {
            // If the requester is a crystal,
            // we want to pay attention to
            // the waitlist, to ensure that everyone
            // gets a turn.
            if ((waitList.Count > 0) && (waitList.Peek() == (object)requester))
            {
                int availablePackets = Mathf.FloorToInt(heldAmount / packetSize);
                reserveAmount = availablePackets * packetSize;
                return availablePackets;
            } else {
                if (!waitList.Contains(requester))
                {
                    waitList.Enqueue(requester);
                }
                return 0;
            }
        } else {
            // If the requester is not a crystal,
            // bypass the waitlist, but honor
            // reservations.
            if ((heldAmount - reserveAmount) > packetSize) {
                return 1;
            } else {
                return 0;
            }
        }
    }

    public bool takeAmount(int amount, Component requester)
    {
        bool canTake = true;
        if ((requester is miCrystal) && waitList.Peek() != (object)requester) {
            canTake = false;
        }

        if (canTake)
        {
            if (heldAmount >= amount)
            {
                if (requester is miCrystal)
                {
                    reserveAmount = 0;
                    waitList.Dequeue();
                }
                heldAmount -= amount;
                return true;
            }
        }
        return false;
    }
}
