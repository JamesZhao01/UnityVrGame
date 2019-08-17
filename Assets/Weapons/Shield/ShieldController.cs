using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
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
        if (other.gameObject.tag == "enemySword")
        {
            Debug.Log("FUCK");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemySword")
        {
            Debug.Log("FUCK2");
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "enemyArrow")
        {
            Destroy(other.gameObject);
        }
    }
}
