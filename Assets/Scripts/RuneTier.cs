using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RuneTier: ScriptableObject {

    // Object type for runes.

    public Sprite tierImage;
    public List<Rune> runes;
    public Color groupColor;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<Rune> GetRunes() {
        return runes;
    }
}
