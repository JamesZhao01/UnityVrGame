using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Chain : MonoBehaviour
{
    ParticleSystem ps;
    Particle[] particles = null;
    public bool activated = true;
    public GameObject target;
    public bool setActivated
    {
        get
        {
            return activated;
        }
        set
        {
            activated = value;
        }
    }

    public void EnableAll()
    {
        EmissionModule mod = gameObject.GetComponent<ParticleSystem>().emission;
        mod.enabled = true;
    }

    public void DisableAll()
    {
        EmissionModule mod = gameObject.GetComponent<ParticleSystem>().emission;
        mod.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            EmissionModule mod = gameObject.GetComponent<ParticleSystem>().emission;
            mod.enabled = true;
            if (target != null)
            {
                //bezier
                Vector3 p0 = gameObject.transform.position;
                Vector3 p3 = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
                Vector3 p1 = Vector3.Lerp(p0, p3, 0.5f);
                Vector3 p2 = p1;

                particles = new Particle[ps.particleCount];

                ps.GetParticles(particles);
                for (int i = 0; i < particles.Length; i++)
                {
                    float t = 1 - particles[i].remainingLifetime / particles[i].startLifetime;
                    particles[i].position = Mathf.Pow(1 - t, 3) * p0 + 3 * Mathf.Pow(1 - t, 2) * t * p1 + 3 * (1 - t) * t * t * p2 + Mathf.Pow(t, 3) * p3;

                }

                ps.SetParticles(particles);
            }
        } else
        {
            EmissionModule mod = gameObject.GetComponent<ParticleSystem>().emission;
            mod.enabled = false;
            target = null;
        }
    }

}
