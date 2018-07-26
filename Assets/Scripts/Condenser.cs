using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condenser : MagicItem {

    [Header("Condenser Settings")]
    [SerializeField]
    Rune rune;
    [SerializeField]
    private MoteEmitter moteEmitter;
    [SerializeField]
    private int charge = 1;

    private int tick = 0;
    private float timer = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime * 0.1f;
        tick++;
        if (tick > 50) {
            tick = 0;
            moteEmitter.EmitMote();
        }
	}

    public void SetRune(Rune newRune)
    {
        rune = newRune;
    }

    public void SetEmitter (MoteEmitter emitter) {
        moteEmitter = emitter;
    }

    public int GetCharge() {
        return charge;
    }
}
