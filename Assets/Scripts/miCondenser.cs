using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miCondenser : MagicItem {

    [Header("Condenser Settings")]
    [SerializeField]
    RuneTier runeTier;
    List<Rune> runes = new List<Rune>();
    [SerializeField]
    float moteRadius;
    [SerializeField]
    float moteProtrusion;
    [SerializeField]
    Focus focus;

    [SerializeField]
    protected GameObject EmitterPrefab;

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

    public void SetFocus(Focus newFocus)
    {
        focus = newFocus;
    }

    public override void CreateChildren()
    {
        Debug.Log("Creating some motes!");
        miMote newMote;
        MoteEmitter newEmitter;
        Renderer newMoteRenderer;
        float theta = (2 * Mathf.PI / runes.Count);
        float xPos;
        float yPos;
        for (int i = 0; i < runes.Count; i++)
        {
            Rune rune = runes[i];
            Debug.Log("Making one for " + rune.name);
            newMote = Instantiate(childPrefab).GetComponent<miMote>();
            newMote.transform.parent = gameObject.transform;
            newMote.name = "Mote-" + rune.name;
            newMote.SetParent(this);
            newMote.SetFocus(focus);
            newMote.SetRune(rune);

            newMoteRenderer = newMote.GetComponentInChildren<Renderer>();
            newMoteRenderer.material.SetColor("_Color", rune.element.groupColor);
            
            children.Add(newMote);

            // Set positioning
            if (runes.Count > 1)
            {
                xPos = Mathf.Sin(theta * i);
                yPos = Mathf.Cos(theta * i);
                newMote.transform.localPosition = new Vector3(xPos * moteRadius, yPos * moteRadius, moteProtrusion);
            }
            else
            {
                newMote.transform.localPosition = new Vector3(0, 0, moteProtrusion);
            }

            // Create the connected Emitter
            newEmitter = Instantiate(EmitterPrefab).GetComponent<MoteEmitter>();
            newEmitter.gameObject.transform.SetParent(focus.emitters.transform,false);
            newEmitter.gameObject.name = "MoteEmitter-" + rune.name;
            newEmitter.SetFocus(focus);
            newEmitter.SetMote(newMote);
            newMote.SetEmitter(newEmitter);

            newMote.transform.LookAt(focus.transform);
            helpers.Add(newEmitter.gameObject);
        }

    }
}
