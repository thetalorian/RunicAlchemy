using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCrystal : MonoBehaviour {


    public float timerOffset;
    public float timer;
    public float height;
    public bool RotActive;
    public GameObject centerpoint;
    private Transform pivot;
    private float startHeight;

    // The Lightening
    public LighteningScript[] lightening;
    private int llife;
    private int lduration;

    // Use this for initialization
	void Start () {
        timer = 0;
        pivot = centerpoint.GetComponent<Transform>();
        startHeight = transform.position.y;

        lduration = 40;
        llife = 0;

	}
	
	// Update is called once per frame
	void Update () {
        if (RotActive)
        {
            timer += Time.deltaTime * 0.4f;
        }
        Rotate();

        foreach (LighteningScript light in lightening) {
            light.enabled = (llife > 0);
        }

        if (llife > 0) {
            llife -= 1;
        }
	}

    void Rotate()
    {
        Vector3 xzpos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 pxzpos = new Vector3(pivot.position.x, 0, pivot.position.z);
        float dist = Vector3.Distance(xzpos, pxzpos);
        //float dist = Vector3.Distance(transform.position, Vector3.zero);
        float x = -Mathf.Cos(timer + timerOffset) * dist + pivot.position.x;
        float z = Mathf.Sin(timer + timerOffset) * dist + pivot.position.z;
        float y = transform.position.y;
        Vector3 pos = new Vector3(x, y, z);
        transform.position = pos;
    }


    public void BringTheLightning() {
        llife = lduration;
    }
}

