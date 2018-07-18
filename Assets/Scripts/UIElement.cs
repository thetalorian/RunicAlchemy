using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Customize(MagicItem target) {
        Text buttonText = GetComponentInChildren<Text>();
        buttonText.text = target.name;
    }
}
