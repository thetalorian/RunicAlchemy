using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crystal Type", menuName ="Crystal Type")]
public class CrystalType : ScriptableObject {

    public new string name;
    public Sprite image;
    public Mesh mesh;
    public int capacity;
}
