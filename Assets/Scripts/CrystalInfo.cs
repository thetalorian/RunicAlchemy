using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalInfo : MonoBehaviour {

    public GameObject runeImage;
    public GameObject crystalImage;
    public Text runeText;
    public Text held;
    public Text capacity;
    public Text refinement;

    Image runeImageComp;
    Image crystalImageComp;


    public Crystal target;
	// Use this for initialization
	void Start () {
        runeImageComp = runeImage.GetComponent<Image>();
        crystalImageComp = crystalImage.GetComponent<Image>();
        changeTarget(target);
	}
	
	// Update is called once per frame
	void Update () {
        held.text = target.heldAmount.ToString();
	}

    public void changeTarget(Crystal targetCrystal) {
        target = targetCrystal;

        runeImageComp.sprite = targetCrystal.rune.GetSprite();
        crystalImageComp.sprite = targetCrystal.crystalSprite;
        runeText.text = targetCrystal.rune.name;
        held.text = targetCrystal.heldAmount.ToString();
        capacity.text = targetCrystal.capacity.ToString();
    }
}
