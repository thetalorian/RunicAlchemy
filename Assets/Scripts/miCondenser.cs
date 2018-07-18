using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miCondenser : MagicItem {

    [SerializeField]
    List<Rune> runes = new List<Rune>();
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

    public void SetRunes(List<Rune> runeList)
    {
        runes = runeList;
    }

    public override void CreateChildren()
    {
        Debug.Log("Creating some motes!");
        GameObject newMote;
        miMote newMoteMI;
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
            
            children.Add(newMoteMI);

            // Set positioning
            xPos = Mathf.Sin(theta * i);
            yPos = Mathf.Cos(theta * i);
            newMote.transform.localPosition = new Vector3(xPos * distance, yPos * distance, above);
            newMote.transform.LookAt(gameObject.transform);
        }

    }
}
