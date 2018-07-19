using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miCondenser : MagicItem {

    [SerializeField]
    RuneTier runeTier;
    [SerializeField]
    List<Rune> runes = new List<Rune>();
    [SerializeField]
    float distance;
    [SerializeField]
    float above;
    [SerializeField]
    Focus focus;

    [SerializeField]
    protected GameObject EmitterPrefab;

    UpgradableStat speed;
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
        GameObject newMote;
        miMote newMoteMI;
        MoteEmitter newEmitter;
        Renderer newMoteRenderer;
        float theta = (2 * Mathf.PI / runes.Count);
        float xPos;
        float yPos;
        for (int i = 0; i < runes.Count; i++)
        {
            Rune rune = runes[i];
            Debug.Log("Making one for " + rune.name);
            newMote = Instantiate(childPrefab);
            newMote.transform.parent = gameObject.transform;
            newMote.name = "Mote-" + rune.name;
            newMoteMI = newMote.GetComponent<miMote>();
            newMoteMI.SetParent(this);
            newMoteMI.SetFocus(focus);

            newMoteRenderer = newMote.GetComponentInChildren<Renderer>();
            newMoteRenderer.material.SetColor("_Color", rune.element.groupColor);
            
            children.Add(newMoteMI);

            // Set positioning
            if (runes.Count > 1)
            {
                xPos = Mathf.Sin(theta * i);
                yPos = Mathf.Cos(theta * i);
                newMote.transform.localPosition = new Vector3(xPos * distance, yPos * distance, above);
            }
            else
            {
                newMote.transform.localPosition = new Vector3(0, 0, above);
            }

            // Create the connected Emitter
            newEmitter = Instantiate(EmitterPrefab).GetComponent<MoteEmitter>();
            newEmitter.gameObject.transform.SetParent(focus.emitters.transform,false);
            newEmitter.gameObject.name = "MoteEmitter-" + rune.name;
            newEmitter.SetFocus(focus);
            newMoteMI.SetEmitter(newEmitter);

            newMote.transform.LookAt(focus.transform);

        }

    }
}
