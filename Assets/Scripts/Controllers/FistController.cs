using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().position = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Enemy"))
        {
            Debug.Log("POW");
            Destroy(other.gameObject);
        }
    }
}
