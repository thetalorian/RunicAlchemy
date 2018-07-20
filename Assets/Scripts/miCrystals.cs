using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miCrystals : MagicItem {

    [SerializeField]
    List<RuneTier> runeTiers = new List<RuneTier>();
    [SerializeField]
    float crystalRadius;
    [SerializeField]
    float above;
    [SerializeField]
    Focus focus;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetFocus(Focus newFocus)
    {
        focus = newFocus;
    }

    public override void CreateChildren()
    {
        Debug.Log("Creating some crystal tiers!");
        miCrystalTier newCrystalTier;


        float offset = 0;
        for (int i = 0; i < runeTiers.Count; i++)
        {
            RuneTier runeTier = runeTiers[i];
            Debug.Log("Making one for " + runeTier.name);
            newCrystalTier = Instantiate(childPrefab).GetComponent<miCrystalTier>();
            newCrystalTier.transform.SetParent(gameObject.transform, false);
            newCrystalTier.gameObject.name = "CrystalTier-" + runeTier.name;
            newCrystalTier.SetParent(this);
            newCrystalTier.SetRunes(runeTier);
            newCrystalTier.SetFocus(focus);
            newCrystalTier.SetOffset(offset);
            offset -= 0.5f;
            children.Add(newCrystalTier);

            newCrystalTier.SetRadius(crystalRadius * i);
            newCrystalTier.CreateChildren();
        }

    }
}
