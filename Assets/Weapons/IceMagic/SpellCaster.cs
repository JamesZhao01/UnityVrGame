using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SpellCaster : InputManagerBase
{
    private float shot = 0f;
    public GameObject wand { get; set; }
    public GameObject castableObject { get; set; }
    public GameObject currentCastable;

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
        Vector3 angularVelocity = pose.GetAngularVelocity(SteamVR_Input_Sources.RightHand);
        Vector3 velocity = pose.GetVelocity(SteamVR_Input_Sources.RightHand);

        if (grab.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            GameObject copy = Object.Instantiate(castableObject, right.transform.position, Quaternion.FromToRotation(Vector3.forward, velocity));
            currentCastable = copy;
        }
        if(grab.GetState(SteamVR_Input_Sources.RightHand))
        {
            currentCastable.GetComponent<Rigidbody>().position = gameObject.transform.position;
            currentCastable.GetComponent<Rigidbody>().rotation = gameObject.transform.rotation;
        }
        if(grab.GetStateUp(SteamVR_Input_Sources.RightHand))
        {
            currentCastable.transform.rotation = Quaternion.FromToRotation(Vector3.forward, velocity);
            currentCastable.GetComponent<IceCrystalController>().Fire(2 * velocity.magnitude * Mathf.Pow(angularVelocity.magnitude, 0.125f));
            Object.Destroy(currentCastable, 2f);
            currentCastable = null;
            shot++;
        }
        if (shot == 3)
        {
            GameObject.Find("WeaponsController").GetComponent<WeaponsController>().currentWeapon = WeaponsController.Weapons.None;
        }
    }

}
