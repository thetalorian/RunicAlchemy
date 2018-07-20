using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miMote : MagicItem {
    [SerializeField]
    Focus focus;
    [SerializeField]
    Rune rune;

    private int tick = 0;
    [SerializeField]
    private MoteEmitter moteEmitter;
    private float timer = 0;

    private int charge = 1;

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

    public void SetFocus(Focus newFocus) {
        focus = newFocus;
    }

    public void SetEmitter (MoteEmitter emitter) {
        moteEmitter = emitter;
    }

    public int GetCharge() {
        return charge;
    }
}
