using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class StringController : MonoBehaviour
{
    public bool flow = true;
    public GameObject p1;
    public GameObject p2;

    public void Start()
    {
        ResetString();
    }

    public void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.InverseTransformPoint(p1.transform.position));
        GetComponent<LineRenderer>().SetPosition(2, transform.InverseTransformPoint(p2.transform.position));
        if (flow)
            ResetString();
    }
    public void ResetString()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.InverseTransformPoint(p1.transform.position));
        GetComponent<LineRenderer>().SetPosition(2, transform.InverseTransformPoint(p2.transform.position));
        GetComponent<LineRenderer>().SetPosition(1, Vector3.Lerp(transform.InverseTransformPoint(p1.transform.position), transform.InverseTransformPoint(p2.transform.position), 0.5f));

    }

}
