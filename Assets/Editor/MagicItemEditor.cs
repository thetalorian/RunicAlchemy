using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MagicItem))]
public class MagicItemEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MagicItem myScript = (MagicItem)target;
        if (GUILayout.Button("Create Children"))
        {
            myScript.CreateChildren();
        }
        if (GUILayout.Button("Kill Children"))
        {
            myScript.KillChildren();
        }
        if (GUILayout.Button("Find Focus"))
        {
            myScript.FindFocus();
        }
    }
}

[CustomEditor(typeof(MagicChamber))]
public class MagicChamberEditor : MagicItemEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}


[CustomEditor(typeof(miCondensers))]
public class miCondensersEditor : MagicItemEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

[CustomEditor(typeof(miCondenser))]
public class miCondenserEditor : MagicItemEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

[CustomEditor(typeof(miCrystals))]
public class miCrystalsEditor : MagicItemEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

[CustomEditor(typeof(miCrystalTier))]
public class miCrystalTierEditor : MagicItemEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

