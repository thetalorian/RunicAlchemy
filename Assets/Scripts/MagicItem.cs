using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicItem : MonoBehaviour
{
    // A magic item is something that is 
    // viewable with Mage vision
    [Header("Magic Item Settings")]
    public MagicItem mageVisionParent;
    public List<MagicItem> children = new List<MagicItem>();
    public List<GameObject> helpers = new List<GameObject>();
    [SerializeField]
    public Vector3 camPosition;
    public bool hasMesh;
    public bool hasDisplay;

    [Space]
    [Header("Magic Item Prefabs")]
    [SerializeField]
    protected GameObject childPrefab;
    [Space]
    [Header("Magic Item Lists")]
    public List<Displays> displays = new List<Displays>();
    public List<UpgradableStat> upgradableStats = new List<UpgradableStat>();
    public List<Bonuses> bonuses = new List<Bonuses>();


    // Magic Items are set up to allow
    // for transition to and from surrounding
    // magic items.
    // Each will have its own potential
    // hierarchy. Basically a multi-leaf
    // tree setup. Which means that each item should
    // have its own parent, possible children,
    // and possible left and right siblings.

    // Most items will have world objects
    // associated with them.
    // Some will only have UI elements.

    // So we'll need to specify the type.

    // Sometimes there will be no actual representation
    // for the object, so we'll want a passthrough type.

    // For example. The basic magic item is just the mage
    // vision controller. That's the standard starting point
    // and the initial target.

    // The children of the mage vision item are a list of
    // the UI element buttons for Altars, Condensers, and Crystals,
    // to start with.

    // However, there is no reason to show all of the condensers, so when
    // we try to choose the condenser level we should pass straight through
    // and automaticall select the currently active condenser.

    // Ok, that means that a Magic Item should also have an active child
    // variable.

    // Curious, though, that means we'll need to specify whether or not
    // we are going up or down in the chain somehow. Actually, no, drop that.
    // Too confusing. We'd be better off just having some representation of
    // "condensers" as a whole. So, drop passthrough. Worst case scenario we can
    // come up with some kind of screen for it.

    // So yeah, if each one has a left and right specified then we can easily shift between items in the chain. An up and a down lets us
    // go back to the parent, or go to the active child.
    //
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetParent(MagicItem newParent)
    {
        mageVisionParent = newParent;
    }

    public virtual void DisplaySmall()
    {
        
    }

    public virtual void DisplayFull()
    {
        
    }

    public virtual void CreateChildren()
    {

    }

    public virtual void StatUpgraded(UpgradableStat stat)
    {
        
    }

    public void KillChildren()
    {
        foreach(MagicItem child in children)
        {
            child.KillChildren();
            DestroyImmediate(child.gameObject);
        }
        children.Clear();

    }

    public void FindFocus()
    {
        Focus foundfocus = MagicChamber.Instance.focus;
        Debug.Log("Found focus: " + foundfocus.name);
    }
}



