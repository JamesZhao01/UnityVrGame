using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SwordController : MonoBehaviour
{
    public float vThresh;
    GameObject weaponsController;
    float life;
    // Start is called before the first frame update
    void Start()
    {
        life = 0f;

    }

    // Update is called once per frame
    void Update()
    {

        life += Time.deltaTime;
        GameObject.Find("Console").GetComponent<Console>().Print(life + "");
        if (life >= 99f)
        {
            GameObject.Find("WeaponsController").GetComponent<WeaponsController>().currentWeapon = WeaponsController.Weapons.None;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Console").GetComponent<Console>().Print("BAM");
        Spawning spawning = GameObject.Find("Terrain").GetComponent<Spawning>();
        if (other.tag.Contains("EnemyHitbox"))
        {
            Debug.Log("shit");
            spawning.DestroyEntity(other.GetComponent<MainHitbox>().enemyGameObject.transform.parent.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        SteamVR_Action_Pose pose = SteamVR_Actions.default_Pose;
        Vector3 v = pose.GetVelocity(SteamVR_Input_Sources.RightHand);
        Vector3 a = pose.GetAngularVelocity(SteamVR_Input_Sources.RightHand);
        Debug.Log("V " + v.ToString("F4"));
        Debug.Log("A " + a.ToString("F4"));
        if (v.magnitude >= vThresh)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(GameObject.Find("Camera").transform.position + (GameObject.Find("Camera").transform.rotation * Vector3.forward).normalized * 2, new Vector3(0.1f, 0.1f, 0.1f));
        }
    }
}
