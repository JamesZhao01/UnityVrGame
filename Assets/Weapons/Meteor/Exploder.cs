using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Finish"))
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            gameObject.transform.Find("meteor").gameObject.SetActive(false);
            gameObject.transform.Find("Impact").gameObject.SetActive(true);
            gameObject.transform.Find("Impact").Find("ImpactSparks").GetComponent<ParticleSystem>().Play();
            gameObject.transform.Find("Impact").Find("Smokering").GetComponent<ParticleSystem>().Play();
            gameObject.transform.Find("Impact").Find("Debris").GetComponent<ParticleSystem>().Play();

            Spawning spawning = GameObject.Find("Terrain").GetComponent<Spawning>();
            List<GameObject> kill = new List<GameObject>();
            foreach (GameObject enemy in spawning.enemiesList)
            {
                if ((enemy.gameObject.transform.position - gameObject.transform.position).magnitude <= 4)
                    kill.Add(enemy);
            }

            foreach (GameObject enemy in kill)
                spawning.DestroyEntity(enemy);
            Destroy(gameObject, 2f);
        }
        

    }
}
