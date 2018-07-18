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

