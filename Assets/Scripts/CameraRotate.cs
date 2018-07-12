using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    public Transform target;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed * 0);

        //transform.Translate(move, Space.World);
        transform.LookAt(target);
        //transform.Translate(Vector3.right * Time.deltaTime);
        transform.Translate(move * Time.deltaTime);
    }

}
