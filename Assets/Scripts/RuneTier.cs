using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneTier : MonoBehaviour {

    // Object type for runes.

    public Sprite tierImage;
    public string tierName;
    public Rune[] runes;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Rune[] GetRunes() {
        return runes;
    }
}
