using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LobCaster : InputManagerBase {
    private float fired = 0f;
    public GameObject lobbable;
    public GameObject clone;
    public override void OnLeftControllerColliderEnter(Collider other)
    {
    }

    public override void OnLeftControllerColliderExit(Collider other)
    {
    }

    public override void OnRightControllerColliderEnter(Collider other)
    {
    }

    public override void OnRightControllerColliderExit(Collider other)
    {
    }

    public override void Update()
    {
        GameObject lobMarker = right.transform.Find("LobMarker").gameObject;
        GameObject handPivot = right.transform.Find("WristPivot").gameObject;
        Vector3 angularVelocity = pose.GetAngularVelocity(SteamVR_Input_Sources.RightHand);
        float dist = (right.transform.Find("LobMarker").transform.position - right.transform.Find("WristPivot").transform.position).magnitude;
        if (grab.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            clone = GameObject.Instantiate(lobbable, lobMarker.transform.position, Quaternion.identity);
            clone.transform.parent = lobMarker.transform;

        }
        if (grab.GetStateUp(SteamVR_Input_Sources.RightHand))
        {
            clone.GetComponent<Rigidbody>().isKinematic = false;
            clone.transform.parent = null;
            Vector3 velocity = angularVelocity * (dist * 0.2f) + 2*pose.GetVelocity(SteamVR_Input_Sources.RightHand);
            clone.GetComponent<Rigidbody>().velocity = velocity;
            clone.GetComponent<Rigidbody>().angularVelocity = angularVelocity;
            fired++;
        }
        if(fired >= 1)
        {
            GameObject.Find("WeaponsController").GetComponent<WeaponsController>().currentWeapon = WeaponsController.Weapons.None;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
