using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMote : MonoBehaviour {

    //public Focus myFocus;
    private float drawSpeed;
    private float rotSpeed;
    private Transform pivot;
    private Focus focus;
    private Condenser myCondenser;
    private int charge;
    private int lifespan;
    private int life;
    private float timer = 0;
    private Material myMaterial;
//    public float rotSpeed = 0.001f;


    // Use this for initialization
    void Start () {
        // When spawning, we need a certain amount of information.

        // We need to determine the position and rotational speed
        // randomly.
        pivot = GameObject.Find("Focus").GetComponent<Transform>();
        rotSpeed = Random.Range(0.4f, 3f);
        timer = Random.Range(0, 360);
        focus = GameObject.Find("Focus").GetComponent<Focus>();
        lifespan = Random.Range(200, 300);
        life = lifespan;

        //pivot = myFocus.GetComponent<Transform>();
//        myMaterial = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime * 0.1f;
        drawSpeed = focus.GetSpeed();
        float step = drawSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pivot.position, step);
        Rotate();
        ShiftSize();
        life -= 1;
        if (life <= 0) {
            die();
        }
	}

    public void setCondenser(Condenser parent) {
        myCondenser = parent;
    }

    public void setCharge(int amount) {
        charge = amount;
    }

    public int getCharge() {
        return charge;
    }

    //public void setFocus(Focus targetFocus){
    //    myFocus = targetFocus;
    //    pivot = myFocus.GetComponent<Transform>();
    //}


//    public void setLifespan(int targetlife){
//        lifespan = targetlife + 50;
//    }

    public void die(){
        myCondenser.MoteCount -= 1;
        Destroy(gameObject);
    }

    //    public void setTimer(float time){
//        timer = time;
//    }

//    public void setSpeed(float speed) {
//        rotSpeed = speed;
//
//    }
    public void setColor(Color targetColor){
        myMaterial = GetComponent<Renderer>().material;
        myMaterial.color = targetColor;
    }

    void Rotate()
    {
        Vector3 xzpos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 pxzpos = new Vector3(pivot.position.x, 0, pivot.position.z);
        float dist = Vector3.Distance(xzpos, pxzpos);
        //float dist = Vector3.Distance(transform.position, Vector3.zero);
        float x = -Mathf.Cos(timer) * dist + pivot.position.x;
        float z = Mathf.Sin(timer) * dist + pivot.position.z;
        float y = transform.position.y;
        Vector3 pos = new Vector3(x, y, z);
        transform.position = pos;
    }

    void ShiftSize() {
        // We want to shift the size of the mote
        // based on its place in its lifespan.

        // To do this we want it to grow steadily
        // during the first 20% of its life, shrink
        // steadily during the last 20%, and stay at
        // full size for the rest.

        float scale = 0;

        float twentypercent = lifespan * .2f;

        if ((life > 0) && (life <= twentypercent)) {
            scale = life / twentypercent;
        }
        if ((life > twentypercent) && (life <= (lifespan-twentypercent))) {
            scale = 1;
        }
        if ((life > (lifespan - twentypercent) && (life <= lifespan))) {
            scale = (lifespan - life) / twentypercent;
        }
        transform.localScale = new Vector3(scale,scale,scale);
    }

}
