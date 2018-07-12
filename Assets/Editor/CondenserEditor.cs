using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Condenser))]
public class CondenserEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Condenser myScript = (Condenser)target;
        if (GUILayout.Button("BuildUI"))
        {
            myScript.BuildUI();
        }
    }

}