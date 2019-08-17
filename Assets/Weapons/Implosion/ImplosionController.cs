using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplosionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {

        if(other.gameObject.tag.Contains("Enemy") || other.gameObject.tag.Contains("Finish"))
        {
            Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);
            gameObject.transform.position = pos;
            GameObject.Find("arenaFloor").GetComponent<Spawning>().Implode(pos, 5);
            Destroy(gameObject, 2);
            gameObject.transform.Find("AirBall").gameObject.SetActive(false);
            GameObject converge = gameObject.transform.Find("Converge").gameObject;
            converge.SetActive(true);
            converge.transform.Find("Convergence").GetComponent<ParticleSystem>().Play();

        }
    }
}


