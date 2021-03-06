﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalTier : MagicItem {

    [SerializeField]
    RuneTier runeTier;
    [SerializeField]
    List<Rune> runes = new List<Rune>();
    [SerializeField]
    float crystalRadius;
    [SerializeField]
    float offset;
    [SerializeField]
    float above;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRadius(float newRadius) {
        crystalRadius = newRadius;
    }

    public void SetRunes(RuneTier newRuneTier)
    {
        runeTier = newRuneTier;
        runes = runeTier.GetRunes();
    }

    public void SetOffset(float newOffset) {
        offset = newOffset;
    }

    public override void CreateChildren()
    {
        Debug.Log("Creating some crystals!");
        Crystal newCrystal;

        float theta = (2 * Mathf.PI / runes.Count);
        float xPos;
        float zPos;

        for (int i = 0; i < runes.Count; i++)
        {
            Rune rune = runes[i];
            Debug.Log("Making one for " + rune.name);

            newCrystal = Instantiate(childPrefab).GetComponent<Crystal>();
            newCrystal.transform.SetParent(gameObject.transform, false);
            newCrystal.gameObject.name = "Crystal-" + rune.name;
            newCrystal.SetParent(this);
            newCrystal.SetRune(rune);
            children.Add(newCrystal);

            xPos = Mathf.Sin(theta * (i + offset));
            zPos = Mathf.Cos(theta * (i + offset));
            newCrystal.transform.localPosition = new Vector3(xPos * crystalRadius, 0, zPos * crystalRadius);

            if (rune.name == "Raw")
            {
                newCrystal.isWell = true;
                MagicChamber.Instance.SetWell(newCrystal);
            }
        }

    }
}
