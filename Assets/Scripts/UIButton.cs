using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : UIElement {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Customize(MagicItem target)
    {
        Text buttonText = GetComponentInChildren<Text>();
        buttonText.text = target.name;
    }

}
