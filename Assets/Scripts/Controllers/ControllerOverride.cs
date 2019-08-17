using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class ControllerOverride : MonoBehaviour
{
    public bool activated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                gameObject.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime, 0, 0);
            }
        }
    }
}
