using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradableStat
{
    // UpgradableStat gives us an encapsulated
    // concept of a statistic that can be
    // upgraded with a cost curve.

    public int level;
    public int initialCost;
    public string upgradeName;
    public string displayName;
    public float growthLinear;
    public float growthQuadratic;
    public MagicItem miParent;

    public UpgradableStat(string name)
    {
        upgradeName = name;
        displayName = upgradeName;
        level = 1;
        initialCost = 1;
    }

    public int GetCost() 
    {
        return Mathf.FloorToInt(Mathf.Pow(growthQuadratic * level, 2) + (growthLinear * level * initialCost) + initialCost);
    }

    public void Upgrade()
    {
        level++;
        if (miParent != null) {
            miParent.StatUpgraded(this);
        }
    }
}
