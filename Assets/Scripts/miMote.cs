using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miMote : MagicItem {
    [SerializeField]
    Focus focus;

    private int tick = 0;
    [SerializeField]
    private MoteEmitter moteEmitter;
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

    public void SetFocus(Focus newFocus) {
        focus = newFocus;
    }

    public void SetEmitter (MoteEmitter emitter) {
        moteEmitter = emitter;
    }
}
