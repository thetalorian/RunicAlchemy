using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mote : MonoBehaviour {
    
    // The Mote object controls creation of motes
    // in the world and their overall settings.


    private Collider focus;
    private float size;
    private int level;
    private Condenser condenser;
    public Rune rune;

    // UI Elments
    private Image runeImage;
    private Image moteImage;
    private Text levelMeter;

    // World Elements
    public GameObject motePrefab;

	// Use this for initialization
	void Start () {
        // Set UI Elements
        runeImage = transform.Find("Rune").GetComponent<Image>();
        moteImage = transform.Find("Mote").GetComponent<Image>();
        levelMeter = transform.Find("Level").GetComponent<Text>();
 
        size = 3.3f;
        focus = GameObject.Find("Focus").GetComponent<Collider>();
        condenser = GetComponentInParent<Condenser>();

        level = 0;
        levelUp();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void levelUp() {
        level += 1;
        levelMeter.text = level.ToString();
    }

    public int getLevel() {
        return level;
    }

    public Sprite getRuneSprite() {
        return runeImage.sprite;
    }

    public Sprite getMoteSprite() {
        return moteImage.sprite;
    }

    public string getRuneName() {
        return rune.name;
    }

    public void SpawnWorldMote(Color cColor, Condenser myCondenser)
    {
        Debug.Log(this.ToString() + "Spawning a worldMote");

        // Select a random position inside a sphere around the focus, but not too close.
        float ranDist = Random.Range(.2f, size);
        float startTime = Random.Range(0, 360);
        float x = -Mathf.Cos(startTime) * ranDist + focus.bounds.center.x;
        float z = Mathf.Sin(startTime) * ranDist + focus.bounds.center.z;
        float ycenter = focus.bounds.center.y;
        float y = Random.Range(ycenter - (size / 4), ycenter + (size / 4));
        Vector3 pos = new Vector3(x, y, z);

        GameObject newMoteGO = (GameObject)Instantiate(motePrefab, pos, Quaternion.identity);
        WorldMote newMote = newMoteGO.GetComponent<WorldMote>();
        newMote.setCondenser(condenser);
        newMote.setCharge(level * condenser.multiplier);
//        newMote.setLifespan(Random.Range(1, 300));
        newMote.setColor(condenser.cColor);
//        MoteCount += 1;
    }
}
