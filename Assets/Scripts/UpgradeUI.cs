using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour {

    public Upgrade upgrade;


    // UI Elements
    private Text levelMeter;
    private Text label;
    private Text cost;

	// Use this for initialization
	void Start () {
        levelMeter = transform.Find("Value").GetComponent<Text>();
        label = transform.Find("Text").GetComponent<Text>();
        cost = transform.Find("Cost").GetComponent<Text>();


	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
