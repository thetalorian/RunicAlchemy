using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSet : MonoBehaviour {
    
    public Upgrade[] upgrades;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Upgrade[] GetUpgrades() {
        return upgrades;
    }
}
