using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Focus : MonoBehaviour {

    // Connect the storage crystal
    public Crystal well;

    // Heat Settings
    public GameObject heatBar;
    Image heatBarFill;
    int heat; // Current heat
    float maxheat; // Maximum heat
    bool overheated; // Overheat status

    // Draw Settings
    bool isDrawing; // Used to determine if the player is currently drawing
    int drawcounter; // Counter for draws.
    int drawrate; // How many draw attempts before working.
    int drawAmount; // How much is drawn with each tick.

    public GameObject emitters;

    // Use this for initialization
    void Start()
    {
        isDrawing = false;
        drawcounter = 0;
        drawrate = 50;
        drawAmount = 0;
        heat = 0;
        maxheat = 250f;
        overheated = false;
        heatBarFill = heatBar.GetComponent<Image>();
    }
    	
	// Update is called once per frame
	void Update () {
        // When drawing, the focus will heat up.
        // When not drawing, it will cool off.
        // If overheated, it cannot be used again until it is completely
        // cooled.
        //Debug.Log("Heat:" + heat + " Overheated: " + overheated);
        // When drawing, magic is added at a rate 
        if (!isDrawing && heat > 0){
            // If the focus is overheated, we need
            // to cool it back down.
            // Also cools if not being heated.
            adjustHeat(-1);
        }
        if (isDrawing && !overheated) {
            addMagic();
            adjustHeat(1);
            if (heat >= maxheat){
                overheated = true;
            }
        }
	}

    private void adjustHeat(int amount)
    {
        heat += amount;
        //Debug.Log(heat);
        float heatpercent = heat / maxheat;
        heatBarFill.fillAmount = heatpercent;
        if (heat >= maxheat){
            overheated = true;
            heatBarFill.color = Color.red;
        }
        if (overheated && heat <= 0){
            heat = 0;
            overheated = false;
            heatBarFill.color = Color.magenta;
        }
        if (heat < 0){
            heat = 0;
        }
    }

    // Turn the focus on and off
	private void OnMouseDown()
	{
        if (!overheated)
        {
            isDrawing = true;
        }
	}

	private void OnMouseUp()
	{
        isDrawing = false;
	}
    public void StartDraw() {
        if (!overheated)
        {
            isDrawing = true;
        }
    }
    public void StopDraw() {
        isDrawing = false;
    }
    void addMagic()
    {
        drawcounter += 1;
        if (drawcounter >= drawrate)
        {
            drawcounter = 0;
            well.addMagic(drawAmount);
        }
    }

    public float GetSpeed(){
        if (isDrawing) {
            return 1f;
        } else {
            return 0f;
        }
    }


	private void OnTriggerEnter(Collider other)
	{
        WorldMote absorbed;
        absorbed = other.GetComponent<WorldMote>();
        if (absorbed) {
            int charge = absorbed.getCharge();
            well.addMagic(charge);
            absorbed.die();
        }
	}

}
