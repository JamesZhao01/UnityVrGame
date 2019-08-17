using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.position = gameObject.transform.position;
        gameObject.transform.rotation = Quaternion.identity;
        rb.rotation = gameObject.transform.rotation;
    }
}
