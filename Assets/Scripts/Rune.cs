using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour {


    public int level;
    public string runename;
    public Sprite sprite;
    public RuneTier tier;
    public RuneElement element;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void levelUp() {
        level += 1;
    }
    public Sprite GetSprite() {
        return sprite;
    }
}
