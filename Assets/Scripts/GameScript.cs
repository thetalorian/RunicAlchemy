using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : MonoBehaviour {

    public void PlayButton (int scene)
    {
        Application.LoadLevel(scene);
    }

    public void ExitButton()
    {
        Application.Quit();

    }

    public void ToggleButton(bool mainMenu)
    {
        if (mainMenu)
            Application.LoadLevel("lv1");
        
    }

}