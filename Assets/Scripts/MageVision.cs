using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageVision : MonoBehaviour {
    // The centralized viewer for
    // game object stats.
    [SerializeField]
    private Canvas inspectionCanvas;

    // Ok, let's prototype this thing
    // out and see what we need.

    // First off, we need to be able
    // to toggle MV on and off.

    // Actually, no. It's always on,
    // since it will need to display
    // standard things.

    // What we toggle on and off is
    // the inspector.

    // The inspector canvas should show us information
    // for the currently selected MagicItem, and give us
    // navigation tools for the next magic item.

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleInspector ()
    {
        inspectionCanvas.enabled = !inspectionCanvas.enabled;
    }
}
