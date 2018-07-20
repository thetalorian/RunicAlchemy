using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miCondensers : MagicItem {

    [Header("Condensers Settings")]
    [SerializeField]
    List<RuneTier> runeTiers = new List<RuneTier>();
    [SerializeField]
    float displacementRadius;
    [SerializeField]
    float condenserHeight;
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
        Debug.Log("Creating some condensers!");
        miCondenser newCondenser;
        Renderer newCondenserRenderer;
        float theta = (2 * Mathf.PI / (runeTiers.Count + 2));
        float xPos;
        float zPos;
        int cPos = 0;
        for (int i = 0; i < runeTiers.Count; i++)
        {
            RuneTier runeTier = runeTiers[i];
            Debug.Log("Making one for " + runeTier.name);
            newCondenser = Instantiate(childPrefab).GetComponent<miCondenser>();
            newCondenser.transform.parent = gameObject.transform;
            newCondenser.name = "Condenser-" + runeTier.name;
            newCondenser.SetParent(this);
            newCondenser.SetRunes(runeTier);
            newCondenser.SetFocus(focus);
            children.Add(newCondenser);

            newCondenserRenderer = newCondenser.GetComponentInChildren<Renderer>();
            newCondenserRenderer.material.SetColor("_Color", runeTier.groupColor);


            // Set positioning
            if (i == 0)
            {
                // This is the Tier0 Condenser, it behaves differently
                newCondenser.transform.localPosition = new Vector3(0, condenserHeight * 1.5f, 0);
            }
            else
            {
                xPos = Mathf.Sin(theta * cPos);
                zPos = Mathf.Cos(theta * cPos);
                newCondenser.transform.localPosition = new Vector3(xPos * displacementRadius, condenserHeight, zPos * displacementRadius);
                cPos++;
                // Skip slots 2 and 5
                if (cPos == 2 || cPos == 5) {
                    cPos++;
                }
            }
            newCondenser.transform.LookAt(focus.transform);
            newCondenser.CreateChildren();
        }

    }
}
