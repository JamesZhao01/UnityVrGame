using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LightningInputManager : InputManagerBase
{
    public float startTime = -1;

    public LightningInputManager()
    {
        startTime = Time.realtimeSinceStartup;
    }
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
        if(Time.realtimeSinceStartup - startTime >= 5f)
        {
            GameObject.Find("WeaponsController").GetComponent<WeaponsController>().currentWeapon = WeaponsController.Weapons.None;
        }
        //haptics.Execute(0f, Time.deltaTime, 100, 5, Valve.VR.SteamVR_Input_Sources.Any);

        GameObject lightning = right.GetComponent<HandController>().GetEquippedObject();
        Spawning spawning = GameObject.Find("Terrain").GetComponent<Spawning>();
        ParticleSystem mainArc = lightning.GetComponentInChildren<ParticleSystem>();


        if (grab.GetState(SteamVR_Input_Sources.RightHand)) {
            right.GetComponent<HandController>().GetEquippedObject().transform.Find("Particle System").gameObject.GetComponent<Arc>().activated = true;
            List<GameObject> enemiesInRange = new List<GameObject>();
            foreach (GameObject enemy in spawning.enemiesList)
            {
                Vector3 directionVector = enemy.transform.Find("cmass").position - right.transform.Find("LightningSource").position;
                Vector3 wandDirection = right.transform.Find("LightningSource").transform.rotation * Vector3.forward;

                float cosVal = Vector3.Dot(directionVector, wandDirection) / directionVector.magnitude / wandDirection.magnitude;
                float theta = Mathf.Acos(cosVal) * Mathf.Rad2Deg;
                if (theta <= 20)
                {
                    enemiesInRange.Add(enemy);
                }
            }
            if (enemiesInRange.Count > 0)
            {
                GameObject selectedEnemy = null;
                float selectedComposite = 10000f;
                GameObject des = GameObject.Find("Destination");

                foreach (GameObject enemy in enemiesInRange)
                {
                    Vector3 directionVector = enemy.transform.Find("cmass").position - right.transform.Find("LightningSource").position;
                    Vector3 wandDirection = right.transform.Find("LightningSource").transform.rotation * Vector3.forward;

                    float cosVal = Vector3.Dot(directionVector, wandDirection) / directionVector.magnitude / wandDirection.magnitude;
                    float theta = Mathf.Acos(cosVal) * Mathf.Rad2Deg;
                    float dist = (directionVector).magnitude;
                    float composite = Mathf.Pow(theta, 4f) * dist;

                    if (selectedEnemy == null)
                    {
                        selectedEnemy = enemy;
                        selectedComposite = composite;
                    }
                    else
                    {
                        if (composite < selectedComposite)
                        {
                            selectedEnemy = enemy;
                            selectedComposite = theta;
                        }
                    }
                }
                lightning.transform.Find("Particle System").GetComponent<Arc>().target = selectedEnemy;
                selectedEnemy.GetComponent<AIController>().Damage(Time.deltaTime * 0.75f);
            } else {
                right.GetComponent<HandController>().GetEquippedObject().transform.Find("Particle System").gameObject.GetComponent<Arc>().target = null;
            }
        } else   {
            right.GetComponent<HandController>().GetEquippedObject().transform.Find("Particle System").gameObject.GetComponent<Arc>().setActivated = false;
            right.GetComponent<HandController>().GetEquippedObject().transform.Find("Particle System").gameObject.GetComponent<Arc>().target = null;
        }

    }
}
