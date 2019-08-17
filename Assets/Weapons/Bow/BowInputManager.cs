using UnityEditor;
using UnityEngine;
using Valve.VR;

public class BowInputManager : InputManagerBase
{
    private int arrowsFired = 0;
    public bool isInfinite = false;

    private bool isColliding = false;
    private bool tracking = false;
    public BowData bowData;
    private GameObject arrowPrefab = null;

    public override void OnLeftControllerColliderEnter(Collider other)
    {
    }
    public override void OnLeftControllerColliderExit(Collider other)
    {
    }
    public override void OnRightControllerColliderEnter(Collider other)
    {
        if (other.name == "StringCollider")
        {
            isColliding = true;
        }
    }
    public override void OnRightControllerColliderExit(Collider other)
    {
        if (other.name == "StringCollider")
        {
            isColliding = false;
        }
    }
    private void ResetBow()
    {
        tracking = false;
        isColliding = false;
        bowData.bow.GetComponent<Drawer>().prog = 0f;
        bowData.ResetBow();
    }
    public override void Update()
    {

        if (isColliding && !tracking && grab.GetState(SteamVR_Input_Sources.RightHand))
        {
            tracking = true;
            bowData.stringCollider.GetComponent<CapsuleCollider>().enabled = false;
            isColliding = false;
        }
        if (tracking)
        {
            if (grab.GetState(SteamVR_Input_Sources.RightHand))
            {
                GameObject handPivot = gameObject.transform.Find("WandHolder").gameObject;
                bowData.stringObject.GetComponent<StringController>().flow = false;
                //set line pos
                bowData.stringObject.GetComponent<LineRenderer>().SetPosition(1, bowData.stringObject.GetComponent<LineRenderer>().transform.InverseTransformPoint(handPivot.transform.position));
                // target loc to right hand
                Vector3 directionVector = bowData.targetLoc.transform.position - handPivot.transform.position;
                // string marker is on resting string
                Vector3 handToStringMarker = handPivot.transform.position - bowData.stringMarker.transform.position;
                // determine direction of hand
                Vector3 localDistance = bowData.gameObject.transform.InverseTransformPoint(handPivot.transform.position) - bowData.gameObject.transform.InverseTransformPoint(bowData.stringMarker.transform.position);

                Vector3 distanceBetweenControlPoints = bowData.cp2.transform.position - bowData.cp1.transform.position;
                bowData.bow.GetComponent<Drawer>().prog = handToStringMarker.magnitude / (0.6f * distanceBetweenControlPoints.magnitude);

                if (localDistance.y < 0.01 || handToStringMarker.magnitude >= 1f * distanceBetweenControlPoints.magnitude)
                {
    
                    ResetBow();
                }
                else
                {
                    Vector3 arrowDistance = bowData.acp2.transform.position - bowData.acp1.transform.position;
                    bowData.arrow.SetActive(true);
                    bowData.arrow.transform.position = handPivot.transform.position;
                    bowData.arrow.transform.rotation = Quaternion.FromToRotation(Vector3.forward, directionVector);
                }

            }
            else
            {
                GameObject handPivot = gameObject.transform.Find("WandHolder").gameObject;
                bowData.stringObject.GetComponent<StringController>().flow = true;
                bowData.arrow.SetActive(false);
                if (arrowPrefab == null)
                {
                    Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Arrow.prefab", typeof(GameObject));
                    arrowPrefab = (GameObject)prefab;
                }
                Vector3 directionVector = (bowData.targetLoc.transform.position - handPivot.transform.position);
                //Quaternion rotation = Quaternion.Euler(0, 2, 0);
                //directionVector = rotation * directionVector;

                GameObject arrowCopy = Object.Instantiate(arrowPrefab, bowData.targetLoc.transform.position, Quaternion.FromToRotation(Vector3.forward, directionVector));
                arrowCopy.GetComponent<Rigidbody>().velocity = directionVector.normalized * Mathf.Pow(directionVector.magnitude, 2) * 30;
                Object.Destroy(arrowCopy, 2);
                ResetBow();
                arrowsFired++;

            }
        }
        if (!isInfinite && arrowsFired == 3)
        {
            GameObject.Find("WeaponsController").GetComponent<WeaponsController>().currentWeapon = WeaponsController.Weapons.None;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
