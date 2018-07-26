using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectors : MagicItem {

    [Header("Collectors Settings")]
    [SerializeField]
    List<RuneTier> runeTiers = new List<RuneTier>();
    [SerializeField]
    float displacementRadius;
    [SerializeField]
    float collectorHeight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void CreateChildren()
    {
        Debug.Log("Creating some collectors!");
        Collector newCollector;
        Renderer newCollectorRenderer;
        float theta = (2 * Mathf.PI / (runeTiers.Count + 2));
        float xPos;
        float zPos;
        int cPos = 0;
        for (int i = 0; i < runeTiers.Count; i++)
        {
            RuneTier runeTier = runeTiers[i];
            Debug.Log("Making one for " + runeTier.name);
            newCollector = Instantiate(childPrefab).GetComponent<Collector>();
            newCollector.transform.parent = gameObject.transform;
            newCollector.name = "Collector-" + runeTier.name;
            newCollector.SetParent(this);
            newCollector.SetRunes(runeTier);
            children.Add(newCollector);

            newCollectorRenderer = newCollector.GetComponentInChildren<Renderer>();
            newCollectorRenderer.material.SetColor("_Color", runeTier.groupColor);


            // Set positioning
            if (i == 0)
            {
                // This is the Tier0 Condenser, it behaves differently
                newCollector.transform.localPosition = new Vector3(0, collectorHeight * 1.5f, 0);
            }
            else
            {
                xPos = Mathf.Sin(theta * cPos);
                zPos = Mathf.Cos(theta * cPos);
                newCollector.transform.localPosition = new Vector3(xPos * displacementRadius, collectorHeight, zPos * displacementRadius);
                cPos++;
                // Skip slots 2 and 5
                if (cPos == 2 || cPos == 5) {
                    cPos++;
                }
            }
            newCollector.transform.LookAt(MagicChamber.Instance.focus.transform);
            newCollector.CreateChildren();
        }

    }
}
