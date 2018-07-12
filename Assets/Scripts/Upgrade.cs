using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour {

    private string upgradeName;
    private int level;
    private int cost;

    // UI Elements
    private Text levelMeter;
    private Text label;
    private Text costDisplay;
    private Button upgradeButton;

    public Upgrade(string name) {
        upgradeName = name;
        level = 1;
        cost = 1;
    }

    public int GetLevel() {
        return level;
    }

    public int GetCost() {
        return cost;
    }

    public void RaiseLevel() {
        level += 1;
        cost += 5;
        costDisplay.text = cost.ToString();
        levelMeter.text = level.ToString();
    }

	// Use this for initialization
	void Start () {
        level = 1;
        cost = 1;
        levelMeter = transform.Find("ValueLabel/Value").GetComponent<Text>();
        label = transform.Find("Label/Text").GetComponent<Text>();
        costDisplay = transform.Find("Upgrade/Cost").GetComponent<Text>();
        upgradeButton = transform.Find("Upgrade").GetComponent<Button>();
        upgradeButton.onClick.AddListener(RaiseLevel);
	}

    public void Initialize(string name)
    {
        upgradeName = name;
        level = 1;
        cost = 1;
        label = transform.Find("Label/Text").GetComponent<Text>();
        label.text = name;
        upgradeButton = transform.Find("Upgrade").GetComponent<Button>();
        upgradeButton.onClick.AddListener(RaiseLevel);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public override string ToString()
    {
        return upgradeName;
    }
}
