using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandController : MonoBehaviour
{
    public bool isLeft;
    public SteamVR_Action_Boolean trigger;
    public SteamVR_Action_Single squeeze;
    public SteamVR_Action_Pose pose;
    public SteamVR_Action_Vector2 vector2;
    public SteamVR_Action_Boolean trackpadClick;
    private GameObject equippedObject;

    public Animator animator;

    public RunesInputManager rim = null;
    public RuneController rc = null;
    private InputManagerBase inputM;
    public InputManagerBase inputManager
    {
        get
        {
            return inputM;
        }
        set
        {
            inputM = value;
            inputM.gameObject = gameObject;
        }
    }

    void Start()
    {
        animator = transform.Find("HandModel").GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

        GetComponentInChildren<Animator>().SetBool("IsFist", trigger.GetState(SteamVR_Input_Sources.LeftHand));
        if (inputManager != null)
            inputManager.Update();
        if(rim != null)
        {
            rim.Update();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (inputManager != null)
        {
            if (isLeft)
            {
                inputManager.OnLeftControllerColliderEnter(other);
            }
            else
            {
                inputManager.OnRightControllerColliderEnter(other);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (inputManager != null)
        {
            if (isLeft)
                inputManager.OnLeftControllerColliderExit(other);
            else
                inputManager.OnRightControllerColliderExit(other);
        }
    }

    public void Equip(GameObject obj, string objectName)
    {
        equippedObject = obj;
        obj.transform.position = gameObject.transform.Find(objectName).position;
        obj.transform.SetParent(gameObject.transform.Find(objectName));
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.localRotation = Quaternion.identity;
    }

    public void EquipRunes(GameObject obj)
    {
        rc = obj.GetComponent<RuneController>();
        obj.transform.SetParent(gameObject.transform.Find("Wrist"));
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.localRotation = Quaternion.identity;
    }

    public void EquipRuneManager(RunesInputManager im)
    {
        rim = im;
    }

    public void UnEquip()
    {
        Destroy(equippedObject);
        equippedObject = null;
    }

    public GameObject GetEquippedObject()
    {
        return equippedObject;
    }

    private void OnDrawGizmos()
    {
        if(inputManager != null)
            inputManager.OnDrawGizmos();
    }
}
