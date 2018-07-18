using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miCondensers : MagicItem {

    [SerializeField]
    List<RuneTier> runeTiers = new List<RuneTier>();
    [SerializeField]
    float distance;
    [SerializeField]
    float above;
    [SerializeField]
    Focus focus;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void CreateChildren()
    {
        Debug.Log("Creating some condensers!");
        GameObject newCondenser;
        miCondenser newCondenserMI;
        Renderer newCondenserRenderer;
        float theta = (2 * Mathf.PI / (runeTiers.Count + 2));
        float xPos;
        float zPos;
        int cPos = 0;
        for (int i = 0; i < runeTiers.Count; i++)
        {
            RuneTier runeTier = runeTiers[i];
            Debug.Log("Making one for " + runeTier.name);
            newCondenser = Instantiate(childPrefab);
            newCondenser.transform.parent = gameObject.transform;
            newCondenser.name = "Condenser-" + runeTier.name;
            newCondenserMI = newCondenser.GetComponent<miCondenser>();
            newCondenserMI.SetParent(this);
            newCondenserMI.SetRunes(runeTier);
            newCondenserMI.SetFocus(focus);
            children.Add(newCondenserMI);

            newCondenserRenderer = newCondenser.GetComponentInChildren<Renderer>();
            newCondenserRenderer.material.SetColor("_Color", runeTier.groupColor);


            // Set positioning
            if (i == 0)
            {
                // This is the Tier0 Condenser, it behaves differently
                newCondenser.transform.localPosition = new Vector3(0, above * 1.5f, 0);
            }
            else
            {
                xPos = Mathf.Sin(theta * cPos);
                zPos = Mathf.Cos(theta * cPos);
                Debug.Log("xPos:" + xPos.ToString() + " zPos: " + zPos.ToString());
                newCondenser.transform.localPosition = new Vector3(xPos * distance, above, zPos * distance);
                cPos++;
                // Skip slots 2 and 5
                if (cPos == 2 || cPos == 5) {
                    cPos++;
                }
            }
            newCondenser.transform.LookAt(focus.transform);
            newCondenserMI.CreateChildren();
        }

    }
}
