using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameBuilder))]
public class GameBuilderEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameBuilder myScript = (GameBuilder)target;
        if (GUILayout.Button("Build Object"))
        {
            myScript.BuildObject();
        }
        if (GUILayout.Button("Build MagicRoom"))
        {
            myScript.BuildMagicRoom();
        }
        if (GUILayout.Button("Build Main Menu"))
        {
            myScript.BuildMainMenu();
        }
    }

}
