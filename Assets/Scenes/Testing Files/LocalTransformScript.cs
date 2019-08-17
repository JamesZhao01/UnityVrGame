using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTransformScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(10, 10, 10);
        Debug.Log(transform.InverseTransformPoint(position));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
