using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public GameObject meteor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Contains("Finish"))
        {
            Destroy(gameObject);
            GameObject.Instantiate(meteor, new Vector3(transform.position.x, transform.position.y + 20, transform.position.z), Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Contains("Finish"))
        {
            Destroy(gameObject);
            for (int i = 0; i < 1; i++)
            {
                GameObject meteorObj = Instantiate(meteor, new Vector3(transform.position.x, transform.position.y + 40, transform.position.z), Quaternion.identity);
                meteorObj.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            }
        }
    }

}
