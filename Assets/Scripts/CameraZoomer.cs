using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomer : MonoBehaviour {

    public float speed;
    public bool inspecting = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (inspecting)
        {
            Vector3 targetPos = transform.parent.position;
            Vector3 targetDir = transform.parent.forward;
            float distance = 1f;

            Vector3 camTarget = targetPos + targetDir * distance;


            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, camTarget, step);
        } else {
            Vector3 camTarget = transform.parent.position;
            float step = speed * 2 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, camTarget, step);
            transform.LookAt(MagicChamber.Instance.focus.transform);
        }
	}
}
