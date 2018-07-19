using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradableStat : UIElement {

    public UpgradableStat targetStat;

    Text labelText;
    Text costText;
    Text levelText;
    Button upgradeButton;

	// Use this for initialization
	void Start () {
        labelText = gameObject.transform.Find("Label").GetComponent<Text>();
        costText = gameObject.transform.Find("UpgradeButton/CostLabel").GetComponent<Text>();
        levelText = gameObject.transform.Find("Level").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Upgrade() {
        targetStat.Upgrade();
        ResetDisplay();

    }

    private void ResetDisplay() {
        costText.text = targetStat.cost.ToString();
        levelText.text = targetStat.level.ToString();
    }

    public override void Customize(UpgradableStat target)
    {
        labelText = gameObject.transform.Find("Label").GetComponent<Text>();
        costText = gameObject.transform.Find("UpgradeButton/CostLabel").GetComponent<Text>();
        levelText = gameObject.transform.Find("Level").GetComponent<Text>();
        upgradeButton = gameObject.transform.GetComponentInChildren<Button>();
        Debug.Log("Customizing Upgradable Stat: " + target.upgradeName);
        targetStat = target;
        Debug.Log(labelText);
        labelText.text = target.upgradeName;
        upgradeButton.onClick.AddListener(Upgrade);
        ResetDisplay();
    }
}
