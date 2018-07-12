using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneEntry : MonoBehaviour
{
    public Rune rune;

    public int level;
    public string name;
    public Text levelIndicator;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void levelUp()
    {
        level += 1;
        updateLevelDisp();
    }

    public void updateLevelDisp()
    {
        levelIndicator.text = level.ToString();
    }

    public Sprite GetSprite()
    {
        Image runeImage = transform.Find("Rune").GetComponent<Image>();
        return runeImage.sprite;
    }
}