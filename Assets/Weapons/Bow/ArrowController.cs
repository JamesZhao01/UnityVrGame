using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject arenaFloor;
    // Start is called before the first frame update
    void Start()
    {
        arenaFloor = GameObject.Find("arenaFloor");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, -2, 0), ForceMode.Acceleration);
        gameObject.GetComponent<Rigidbody>().rotation = Quaternion.FromToRotation(Vector3.forward, gameObject.GetComponent<Rigidbody>().velocity);
    }

  
}
