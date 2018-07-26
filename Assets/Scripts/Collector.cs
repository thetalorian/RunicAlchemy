using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MagicItem {

    [Header("Collector Settings")]
    [SerializeField]
    RuneTier runeTier;
    List<Rune> runes = new List<Rune>();
    [SerializeField]
    float condenserRadius;
    [SerializeField]
    float condenserProtrusion;

    [SerializeField]
    protected GameObject EmitterPrefab;
    [Space]
    [SerializeField]
    UpgradableStat speed;
    [SerializeField]
    UpgradableStat max;

    // Use this for initialization
    void Start () {
        speed = new UpgradableStat("Speed");
        max = new UpgradableStat("Max");
        upgradableStats.Add(speed);
        upgradableStats.Add(max);
	}

	// Update is called once per frame
	void Update () {
		
	}

    public void SetRunes(RuneTier newRuneTier)
    {
        runeTier = newRuneTier;
        runes = runeTier.GetRunes();
    }

    public override void CreateChildren()
    {
        Debug.Log("Creating some condensers!");
        Condenser newCondenser;
        MoteEmitter newEmitter;
        Renderer newCondenserRenderer;
        float theta = (2 * Mathf.PI / runes.Count);
        float xPos;
        float yPos;
        for (int i = 0; i < runes.Count; i++)
        {
            Rune rune = runes[i];
            Debug.Log("Making one for " + rune.name);
            newCondenser = Instantiate(childPrefab).GetComponent<Condenser>();
            newCondenser.transform.parent = gameObject.transform;
            newCondenser.name = "Condenser-" + rune.name;
            newCondenser.SetParent(this);
            newCondenser.SetRune(rune);

            newCondenserRenderer = newCondenser.GetComponentInChildren<Renderer>();
            newCondenserRenderer.material.SetColor("_Color", rune.element.groupColor);
            
            children.Add(newCondenser);

            // Set positioning
            if (runes.Count > 1)
            {
                xPos = Mathf.Sin(theta * i);
                yPos = Mathf.Cos(theta * i);
                newCondenser.transform.localPosition = new Vector3(xPos * condenserRadius, yPos * condenserRadius, condenserProtrusion);
            }
            else
            {
                newCondenser.transform.localPosition = new Vector3(0, 0, condenserProtrusion);
            }

            // Create the connected Emitter
            newEmitter = Instantiate(EmitterPrefab).GetComponent<MoteEmitter>();
            newEmitter.gameObject.transform.SetParent(MagicChamber.Instance.focus.emitters.transform,false);
            newEmitter.gameObject.name = "MoteEmitter-" + rune.name;
            newEmitter.SetCondenser(newCondenser);
            newCondenser.SetEmitter(newEmitter);

            newCondenser.transform.LookAt(MagicChamber.Instance.focus.transform);
            helpers.Add(newEmitter.gameObject);
        }

    }
}
