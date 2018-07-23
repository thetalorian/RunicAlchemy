using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
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
    [Space]
    public Focus focus;
    public miCrystal well;

    private static MagicChamber _instance;
    public static MagicChamber Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("MagicChamber").GetComponent<MagicChamber>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

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

    public void SetWell(miCrystal newWell)
    {
        well = newWell;
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
        children.Add(newCondensers);
        newCondensers.CreateChildren();

        // Then the crystals
        miCrystals newCrystals = Instantiate(crystalsPrefab).GetComponent<miCrystals>();
        newCrystals.transform.SetParent(gameObject.transform, false);
        newCrystals.name = "Crystals";
        newCrystals.SetParent(this);
        children.Add(newCrystals);
        newCrystals.CreateChildren();

        // Then the altars



    }
}
