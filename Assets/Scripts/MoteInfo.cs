using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoteInfo : MonoBehaviour {

    public Mote target;

    private Image moteImage;
    private Image runeImage;
    private Text runeName;
    private Text levelMeter;

    // Use this for initialization
    void Start()
    {
        runeImage = transform.Find("Rune").GetComponent<Image>();
        runeName = transform.Find("RuneName").GetComponent<Text>();
        moteImage = transform.Find("Mote").GetComponent<Image>();
        levelMeter = transform.Find("Level").GetComponent<Text>();
        changeTarget(target);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void changeTarget(Mote targetMote)
    {
        target = targetMote;
        moteImage.sprite = target.getMoteSprite();
        runeImage.sprite = target.getRuneSprite();
        runeName.text = target.getRuneName();
        levelMeter.text = target.getLevel().ToString();
    }

    public void LevelUp() {
        target.levelUp();
        levelMeter.text = target.getLevel().ToString();
    }

}
