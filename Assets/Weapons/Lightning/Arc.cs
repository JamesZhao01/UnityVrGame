using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Arc : MonoBehaviour
{

    Vector3 a0;
    Vector3 a1;
    Vector3 a2;
    Vector3 a3;
    Particle[] particles = null;
    public GameObject target;
    public bool activated = true;
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
    public ParticleSystem ps;
    public GameObject chainReference;
    public GameObject chain1;
    public GameObject chain2;
    public GameObject chain1p;
    public GameObject chain2p;

    public GameObject shit1;
    public GameObject shit2;

    public GameObject closest1o;
    public GameObject closest2o;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        chain1 = Instantiate(chainReference);
        chain2 = Instantiate(chainReference);
        chain1p = chain1.transform.Find("Particle System").gameObject;
        chain2p = chain2.transform.Find("Particle System").gameObject;
    }

    void DisableAll()
    {
        EmissionModule mod = gameObject.GetComponent<ParticleSystem>().emission;
        chain1p.GetComponent<Chain>().activated = false;
        chain2p.GetComponent<Chain>().activated = false;
        mod.enabled = false;
    }

    void EnableAll()
    {
        EmissionModule mod = gameObject.GetComponent<ParticleSystem>().emission;
        chain1p.GetComponent<Chain>().activated = true;
        chain2p.GetComponent<Chain>().activated = true;
        mod.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            EmissionModule mod = gameObject.GetComponent<ParticleSystem>().emission;
            mod.enabled = true;
            if (target == null)
            {
                chain1p.GetComponent<Chain>().activated = false;
                chain1p.GetComponent<Chain>().target = null;
                chain2p.GetComponent<Chain>().activated = false;
                chain2p.GetComponent<Chain>().target = null;
            }
            else
            {
                Spawning arenaFloor = GameObject.Find("Terrain").GetComponent<Spawning>();
                Vector3 p0 = gameObject.transform.position;
                Vector3 p1 = p0 + (gameObject.transform.rotation * Vector3.forward).normalized * 2;
                Vector3 p2 = p1;
                Vector3 p3 = new Vector3(target.transform.position.x + Random.Range(-0.1f, 0.1f), target.transform.position.y + 1 + Random.Range(-0.2f, 0.2f), target.transform.position.z + Random.Range(-0.1f, 0.1f));

                a0 = p0;
                a1 = p1;
                a2 = p2;
                a3 = p3;
                particles = new Particle[ps.particleCount];

                ps.GetParticles(particles);
                for (int i = 0; i < particles.Length; i++)
                {
                    float t = 1 - particles[i].remainingLifetime / particles[i].startLifetime;
                    particles[i].position = Mathf.Pow(1 - t, 3) * p0 + 3 * Mathf.Pow(1 - t, 2) * t * p1 + 3 * (1 - t) * t * t * p2 + Mathf.Pow(t, 3) * p3;
                    //particles[i].position = Vector3.Lerp(p0, p3, 1 - particles[i].remainingLifetime / particles[i].startLifetime);

                }

                ps.SetParticles(particles);

                List<GameObject> enemies = arenaFloor.enemiesList;
                List<GameObject> acceptable = new List<GameObject>();
                for (int i = 0; i < enemies.Count; i++)
                {
                    if ((enemies[i].transform.position - target.transform.position).magnitude <= 4)
                    {
                        acceptable.Add(enemies[i]);
                    }
                }
                if (acceptable.Count >= 1)
                {
                    GameObject closest = null;
                    GameObject closest2 = null;
                    for (int i = 0; i < acceptable.Count; i++)
                    {
                        if (ReferenceEquals(acceptable[i], target))
                            continue;
                        if (closest == null)
                        {
                            closest = acceptable[i];
                            continue;

                        }
                        if ((acceptable[i].transform.position - target.transform.position).magnitude < (closest.transform.position - target.transform.position).magnitude)
                        {
                            closest2 = closest;
                            closest = acceptable[i];

                        }
                            
                    }
                    closest1o = closest;
                    closest2o = closest2;
                    if (closest != null)
                    {
                        chain1p.GetComponent<Chain>().activated = true;
                        chain1.transform.position = target.transform.Find("cmass").transform.position;

                        chain1p.GetComponent<Chain>().target = closest.transform.Find("cmass").gameObject;
                        chain1.transform.rotation = Quaternion.FromToRotation(Vector3.forward, chain1p.GetComponent<Chain>().target.transform.position - target.transform.Find("cmass").position);
                        closest.GetComponent<AIController>().Damage(Time.deltaTime * 0.5f);

                    } else
                    {
                        chain1p.GetComponent<Chain>().activated = false;
                        chain1p.GetComponent<Chain>().target = null;
                    }
                    if (closest2 != null)
                    {
                        chain2p.GetComponent<Chain>().activated = true;
                        chain2.transform.position = target.transform.Find("cmass").transform.position;

                        chain2p.GetComponent<Chain>().target = closest2.transform.Find("cmass").gameObject;
                        chain2.transform.rotation = Quaternion.FromToRotation(Vector3.forward, chain2p.GetComponent<Chain>().target.transform.position - target.transform.Find("cmass").position);
                        closest2.GetComponent<AIController>().Damage(Time.deltaTime * 0.5f);
                    }
                    else
                    {
                        chain2p.GetComponent<Chain>().activated = false;
                        chain2p.GetComponent<Chain>().target = null;
                    }


                }
                else if (enemies.Count == 0)
                {
                    chain1p.GetComponent<Chain>().activated = false;
                    chain2p.GetComponent<Chain>().activated = false;
                }

            }
        } else
        {
            EmissionModule mod = gameObject.GetComponent<ParticleSystem>().emission;
            mod.enabled = true;
            chain1p.GetComponent<Chain>().activated = false;
            chain1p.GetComponent<Chain>().target = null;
            chain2p.GetComponent<Chain>().activated = false;
            chain2p.GetComponent<Chain>().target = null;
        }
        
    }

}