using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicChamber : MagicItem {

    [Header("Magic Chamber Prefabs")]
    [SerializeField]
    GameObject focusPrefab;
    [SerializeField]
    GameObject condensersPrefab;
    [SerializeField]
    GameObject crystalsPrefab;
    [SerializeField]
    GameObject altarsPrefab;
    [SerializeField]
    Focus focus;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetFocus(Focus newFocus)
    {
        focus = newFocus;
    }
    public override void CreateChildren()
    {
        Debug.Log("Setting Up the Magic Chamber!");

        // First the focus
        Focus newFocus = Instantiate(focusPrefab).GetComponent<Focus>();
        newFocus.transform.SetParent(gameObject.transform, false);
        newFocus.name = "Focus";
        newFocus.SetParent(this);
        children.Add(newFocus);
        SetFocus(newFocus);

        // Then the condensers
        miCondensers newCondensers = Instantiate(condensersPrefab).GetComponent<miCondensers>();
        newCondensers.transform.SetParent(gameObject.transform, false);
        newCondensers.name = "Condensers";
        newCondensers.SetParent(this);
        newCondensers.SetFocus(focus);
        children.Add(newCondensers);
        newCondensers.CreateChildren();

        // Then the crystals
        miCrystals newCrystals = Instantiate(crystalsPrefab).GetComponent<miCrystals>();
        newCrystals.transform.SetParent(gameObject.transform, false);
        newCrystals.name = "Crystals";
        newCrystals.SetParent(this);
        newCrystals.SetFocus(focus);
        children.Add(newCrystals);
        newCrystals.CreateChildren();

        // Then the altars



    }
}
