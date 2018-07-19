using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoteEmitter : MonoBehaviour {

    [SerializeField]
    private ParticleSystem particleSystem;
    [SerializeField]
    private miMote miMote;
    [SerializeField]
    private Focus focus; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveMotes();
	}

    public void SetMote(miMote mote) {
        miMote = mote;
    }

    public void EmitMote() {
        particleSystem.Emit(1);
    }

    public void SetFocus(Focus newFocus) {
        focus = newFocus;
    }

    public void MoveMotes() {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        int particleCount = particleSystem.GetParticles(particles);

        float drawSpeed = focus.GetSpeed();
        float step = drawSpeed * Time.deltaTime;
        if (drawSpeed > 0)
        {
            for (int i = 0; i < particleCount; i++)
            {
                //particles[i].position = Vector3.MoveTowards(particles[i].position, focus.transform.localPosition, step);
                particles[i].position = Vector3.MoveTowards(particles[i].position, new Vector3(0,0,0), step);
            }
            particleSystem.SetParticles(particles, particleCount);
        }
    }
}
