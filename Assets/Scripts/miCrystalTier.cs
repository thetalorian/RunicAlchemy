using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miCrystalTier : MagicItem {

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
    [SerializeField]
    Focus focus;

    [SerializeField]
    protected GameObject EmitterPrefab;

    UpgradableStat speed;
    UpgradableStat max;

    // Use this for initialization
    void Start()
    {
        speed = new UpgradableStat("Speed");
        max = new UpgradableStat("Max");
        upgradableStats.Add(speed);
        upgradableStats.Add(max);
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

    public void SetFocus(Focus newFocus)
    {
        focus = newFocus;
    }

    public void SetOffset(float newOffset) {
        offset = newOffset;
    }

    public override void CreateChildren()
    {
        Debug.Log("Creating some crystals!");
        miCrystal newCrystal;

        float theta = (2 * Mathf.PI / runes.Count);
        float xPos;
        float zPos;

        for (int i = 0; i < runes.Count; i++)
        {
            Rune rune = runes[i];
            Debug.Log("Making one for " + rune.name);

            newCrystal = Instantiate(childPrefab).GetComponent<miCrystal>();
            newCrystal.transform.SetParent(gameObject.transform, false);
            newCrystal.gameObject.name = "Crystal-" + rune.name;
            newCrystal.SetParent(this);
            newCrystal.SetFocus(focus);
            newCrystal.SetRune(rune);
            children.Add(newCrystal);

            xPos = Mathf.Sin(theta * (i + offset));
            zPos = Mathf.Cos(theta * (i + offset));
            newCrystal.transform.localPosition = new Vector3(xPos * crystalRadius, 0, zPos * crystalRadius);

            if (rune.name == "Raw")
            {
                newCrystal.isWell = true;
                focus.well = newCrystal;
            }
        }

    }
}
