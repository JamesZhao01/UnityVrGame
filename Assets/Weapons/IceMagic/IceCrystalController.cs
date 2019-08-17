using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCrystalController : MonoBehaviour
{

    private bool fired = false;
    public float speed = 1000f;
    public void Fire(Vector3 direction)
    {
        fired = true;
        gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(direction.x*100, direction.y*100, direction.z*100);
    }

    public void Fire(float mag)
    {
        fired = true;
        Vector3 directionVector = transform.rotation * Vector3.forward;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(directionVector.x * mag, directionVector.y * mag, directionVector.z * mag);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(fired)
        {
            gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.forward, gameObject.GetComponent<Rigidbody>().velocity);
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, -3, 0), ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("Enemy"))
        {
            other.GetComponent<AIController>().Root(10f);
            GameObject.Find("Console").GetComponent<Console>().Print("FREEZE");
        }
    }
}
