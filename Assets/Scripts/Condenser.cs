using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condenser : MonoBehaviour
{

    public Upgrade speed;
    public Upgrade maxMotes;
    public int MaxMotes;
    public int MoteCount;
    public Mote[] motes;
    public Color cColor;
    public int multiplier;
    public RuneTier runeTier;

    //    private Upgrade[] upgrades = new Upgrade[2];
//    upgrades[0] = new Upgrade("speed");
//    upgrades[1] = new Upgrade("max");

//    Upgrade upgrade1 = new Upgrade("speed");
//    Upgrade upgrade2 = new Upgrade("max");

    private int tick;
    private int spawnTick;
    private int maxTick;

	// Use this for initialization
	void Start () {
        maxTick = 100;
        tick = 0;
        spawnTick = Random.Range(1, maxTick);
	}

    // Update is called once per frame
    void Update()
    {
        tick += 1;
        if (tick >= spawnTick && MoteCount < MaxMotes) {
            SpawnMote();
            tick = 0;
            spawnTick = Random.Range(1, maxTick);
        }
    }

    public void SpawnMote()
    {
        // Choose a mote to spawn.
        int whichOne = Random.Range(0, motes.Length);

        Debug.Log(this.ToString() + "Chose to spawn: " + motes[whichOne].ToString() + ". Mote len: " + motes.Length.ToString() + ", chose " + whichOne.ToString());

        motes[whichOne].SpawnWorldMote(cColor, this);
        MoteCount += 1;
//        // Select a random position inside a sphere around the focus, but not too close.
//        float ranDist = Random.Range(.2f, size);
//        float startTime = Random.Range(0, 360);
//        float x = -Mathf.Cos(startTime) * ranDist + focusCollider.bounds.center.x;
//        float z = Mathf.Sin(startTime) * ranDist + focusCollider.bounds.center.z;
//        float ycenter = focusCollider.bounds.center.y;
//        float y = Random.Range(ycenter - (size / 4), ycenter + (size / 4));
//        Vector3 pos = new Vector3(x, y, z);


//        //Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
//        GameObject newMoteGO = (GameObject)Instantiate(motePrefab, pos, Quaternion.identity);
//        Mote newMote = newMoteGO.GetComponent<Mote>();
//        newMote.setCondenser(this);
//        newMote.setCharge(Random.Range(1, 5));
//        newMote.setFocus(moteFocus);
//        newMote.setTimer(startTime);
//        newMote.setSpeed(Random.Range(0.4f, 3f));
//        newMote.setLifespan(Random.Range(1, 300));
//        newMote.setColor(cColor);
//        MoteCount += 1;
    }


    //	private void OnDrawGizmosSelected()
    //	{
    //        Gizmos.color = new Color(1, 0, 0, 0.5f);
    //        Gizmos.DrawSphere(focusCollider.bounds.center, size);
    //	}

    public void BuildUI()
    {

        GameObject UIPanel = (GameObject)Resources.Load("UIPanel", typeof(GameObject));
        Vector3 center = new Vector3(0, 0, 0);
        Vector2 none = new Vector2(0, 0);
        Vector2 all = new Vector2(1, 1);
        Vector3 full = new Vector3(1, 1, 1);

        // Set up Mote Tab
        GameObject moteTab = Instantiate(UIPanel, center, Quaternion.identity);
        moteTab.name = "moteTab";
        moteTab.transform.SetParent(this.transform);

        RectTransform moteTabRT = moteTab.GetComponent<RectTransform>();
        moteTabRT.anchorMin = none;
        moteTabRT.anchorMax = all;
        moteTabRT.offsetMin = none;
        moteTabRT.offsetMax = none;
        moteTabRT.localScale = full;

        // Create Motes
        List<Rune> myRunes = runeTier.GetRunes();
        motes = new Mote[myRunes.Count];

        // We need to determine our lanes.
        float marginOffset = 0.05f;
        int rows = ((myRunes.Count - 1) / 3) + 1;
        int cols = ((myRunes.Count - 1) % 3) + 1;

        float rowsize = (1 - (marginOffset * 2)) / rows;
        float colsize = (1 - (marginOffset * 2)) / cols;

        GameObject thisMote;
        RectTransform thisMoteRT;
        GameObject motePrefab = (GameObject)Resources.Load("Mote", typeof(GameObject));
        for (int i = 0; i < myRunes.Count; i++)
        {
            Rune rune = myRunes[i];
            int rowpos = i / 3;
            int colpos = i % 3;
            thisMote = Instantiate(motePrefab, center, Quaternion.identity);
            thisMote.name = "Mote-" + rune.name;

            Mote moteScript = thisMote.GetComponent<Mote>();
            moteScript.rune = rune;
            thisMote.transform.SetParent(moteTab.transform);
            thisMoteRT = thisMote.GetComponent<RectTransform>();
            thisMoteRT.anchorMin = new Vector2(marginOffset + (colpos * colsize), 1 - (marginOffset + ((rowpos + 1) * rowsize)));
            thisMoteRT.anchorMax = new Vector2(marginOffset + ((colpos + 1) * colsize), 1 - (marginOffset + (rowpos * rowsize)));
            Image runeImage = thisMote.transform.Find("Display/Rune").GetComponent<Image>();
            runeImage.sprite = rune.GetSprite();

            thisMoteRT.offsetMin = none;
            thisMoteRT.offsetMax = none;
            thisMoteRT.localScale = full;

        }

        // Set up Upgrade Tab
        GameObject upgradeTab = Instantiate(UIPanel, center, Quaternion.identity);
        upgradeTab.name = "upgradeTab";
        upgradeTab.transform.SetParent(this.transform);

        RectTransform upgradeTabRT = upgradeTab.GetComponent<RectTransform>();
        upgradeTabRT.anchorMin = none;
        upgradeTabRT.anchorMax = all;
        upgradeTabRT.offsetMin = none;
        upgradeTabRT.offsetMax = none;
        upgradeTabRT.localScale = full;

        // Create Upgrades
        GameObject thisUpgrade;
        RectTransform thisUpgradeRT;
        GameObject upgradePrefab = (GameObject)Resources.Load("Upgrade", typeof(GameObject));

        speed = Instantiate(upgradePrefab, center, Quaternion.identity).GetComponent<Upgrade>();
        speed.Initialize("Speed");
        speed.gameObject.name = "Speed";
        maxMotes = Instantiate(upgradePrefab, center, Quaternion.identity).GetComponent<Upgrade>();
        maxMotes.Initialize("Max");
        maxMotes.gameObject.name = "maxMotes";

        Upgrade[] upgrades = new Upgrade[2] { speed, maxMotes };

        // We need to determine our lanes.
        marginOffset = 0.05f;
        rows = ((upgrades.Length - 1) / 3) + 1;
        cols = ((upgrades.Length - 1) % 3) + 1;
        rowsize = (1 - (marginOffset * 2)) / rows;
        colsize = (1 - (marginOffset * 2)) / cols;


        for (int i = 0; i < upgrades.Length;i++) {
            Debug.Log(upgrades[i].ToString());
            int rowpos = i / 3;
            int colpos = i % 3;
            upgrades[i].transform.SetParent(upgradeTab.transform);
            RectTransform thisRT = upgrades[i].gameObject.GetComponent<RectTransform>();
            thisRT.anchorMin = new Vector2(marginOffset + (colpos * colsize), 1 - (marginOffset + ((rowpos + 1) * rowsize)));
            thisRT.anchorMax = new Vector2(marginOffset + ((colpos + 1) * colsize), 1 - (marginOffset + (rowpos * rowsize)));
            thisRT.offsetMin = none;
            thisRT.offsetMax = none;
            thisRT.localScale = full;
        }

 //       GameObject thisUpgrade;
//        RectTransform thisUpgradeRT;
//        GameObject upgradePrefab = (GameObject)Resources.Load("Upgrade", typeof(GameObject));
//        for (int i = 0; i < upgradelist.Length; i++)
//        {
//            string currentUpgrade = upgradelist[i];
//            int rowpos = i / 3;
//            int colpos = i % 3;
//            thisUpgrade = Instantiate(upgradePrefab, center, Quaternion.identity);
//            thisUpgrade.name = "Upgrade-" + currentUpgrade;

//            Upgrade upgradeScript = thisUpgrade.GetComponent<Upgrade>();
//            //moteScript.rune = rune;
//            thisUpgrade.transform.SetParent(upgradeTab.transform);
//            thisUpgradeRT = thisUpgrade.GetComponent<RectTransform>();
//            thisUpgradeRT.anchorMin = new Vector2(marginOffset + (colpos * colsize), 1 - (marginOffset + ((rowpos + 1) * rowsize)));
//            thisUpgradeRT.anchorMax = new Vector2(marginOffset + ((colpos + 1) * colsize), 1 - (marginOffset + (rowpos * rowsize)));
            //Image runeImage = thisMote.transform.Find("Display/Rune").GetComponent<Image>();
            //runeImage.sprite = rune.GetSprite();

//            thisUpgradeRT.offsetMin = none;
//            thisUpgradeRT.offsetMax = none;
//            thisUpgradeRT.localScale = full;

//        }

    }
}
