using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoteEmitter : MonoBehaviour {

    [SerializeField]
    private ParticleSystem moteParticleSystem;
    [SerializeField]
    private Condenser condenser;

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

    public void SetCondenser(Condenser newCondenser) 
    {
        condenser = newCondenser;
    }

    public void EmitMote() 
    {
        moteParticleSystem.Emit(1);
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(moteParticleSystem, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            MagicChamber.Instance.well.addMagic(condenser.GetCharge());
        }
    }

    public void MoveMotes() 
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[moteParticleSystem.main.maxParticles];
        int particleCount = moteParticleSystem.GetParticles(particles);

        float drawSpeed = MagicChamber.Instance.focus.GetSpeed();
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
