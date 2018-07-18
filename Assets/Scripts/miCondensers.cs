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
        float theta = (2 * Mathf.PI / runeTiers.Count);
        float xPos;
        float zPos;
        for (int i = 0; i < runeTiers.Count; i++)
        {
            RuneTier runeTier = runeTiers[i];
            Debug.Log("Making one for " + runeTier.name);
            newCondenser = Instantiate(childPrefab);
            newCondenser.transform.parent = gameObject.transform;
            newCondenser.name = "Condenser-" + runeTier.name;
            newCondenserMI = newCondenser.GetComponent<miCondenser>();
            newCondenserMI.SetParent(this);
            newCondenserMI.SetRunes(runeTier.GetRunes());
            children.Add(newCondenserMI);

            // Set positioning
            xPos = Mathf.Sin(theta * i);
            zPos = Mathf.Cos(theta * i);
            Debug.Log("xPos:" + xPos.ToString() + " zPos: " + zPos.ToString());
            newCondenser.transform.localPosition = new Vector3(xPos * distance, above, zPos * distance);
            newCondenser.transform.LookAt(gameObject.transform);

            newCondenserMI.CreateChildren();
        }

    }
}
