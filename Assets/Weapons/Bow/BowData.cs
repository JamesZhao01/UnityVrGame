using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowData : MonoBehaviour
{
    public GameObject stringCollider;
    public GameObject stringMarker;
    public GameObject stringObject;
    public GameObject arrow;
    public GameObject bow;
    public GameObject cp1;
    public GameObject cp2;
    public GameObject targetLoc;
    public GameObject acp1;
    public GameObject acp2;

    public void ResetBow()
    {
        bow.GetComponent<Drawer>().prog = 0f;
        stringObject.GetComponent<StringController>().ResetString();
        arrow.SetActive(false);
        stringCollider.GetComponent<CapsuleCollider>().enabled = true;

    }
}
