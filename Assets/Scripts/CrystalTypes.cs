using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crystal Types", menuName = "Crystal Types")]
public class CrystalTypes : ScriptableObject {

    public List<CrystalType> crystalTypes;

    public CrystalType GetTypeForSize(int size) 
    {
        if (size > (crystalTypes.Count - 1)) {
            return crystalTypes[crystalTypes.Count - 1];
        } else {
            return crystalTypes[size];
        }
    }
}
