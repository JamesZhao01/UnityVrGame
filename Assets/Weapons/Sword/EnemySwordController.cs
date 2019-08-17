using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordController : MonoBehaviour
{
    GameObject weaponsController;
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
        if(other.tag == "PlayerHitbox")
        {
            //GameObject.Find("SceneController").GetComponent<SceneHandler>().Dead();
        }
    }
}
