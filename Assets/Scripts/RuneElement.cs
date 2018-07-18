﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneElement : MonoBehaviour {

    // Object type for runes.

    public Sprite elementImage;
    public string elementName;
    public List<Rune> runes;
    public Color groupColor;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Rune> GetRunes()
    {
        return runes;
    }

}
