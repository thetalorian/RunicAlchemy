﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoteEmitter : MonoBehaviour {

    [SerializeField]
    private ParticleSystem moteParticleSystem;
    [SerializeField]
    private miMote miMote;
    [SerializeField]
    private Focus focus;

    List<ParticleCollisionEvent> collisionEvents;

	// Use this for initialization
	void Start () 
    {
        moteParticleSystem = gameObject.GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        MoveMotes();
	}

    public void SetMote(miMote mote) 
    {
        miMote = mote;
    }

    public void EmitMote() 
    {
        moteParticleSystem.Emit(1);
    }

    public void SetFocus(Focus newFocus) 
    {
        focus = newFocus;
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(moteParticleSystem, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            focus.well.addMagic(miMote.GetCharge());
        }
    }

    public void MoveMotes() 
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[moteParticleSystem.main.maxParticles];
        int particleCount = moteParticleSystem.GetParticles(particles);

        float drawSpeed = focus.GetSpeed();
        float step = drawSpeed * Time.deltaTime;
        if (drawSpeed > 0)
        {
            for (int i = 0; i < particleCount; i++)
            {
                //particles[i].position = Vector3.MoveTowards(particles[i].position, focus.transform.localPosition, step);
                particles[i].position = Vector3.MoveTowards(particles[i].position, new Vector3(0,0,0), step);
            }
            moteParticleSystem.SetParticles(particles, particleCount);
        }
    }
}