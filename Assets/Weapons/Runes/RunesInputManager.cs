using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using System.Text;

public class RunesInputManager : InputManagerBase
{
    private float duration = 6f;
    private float startingTime = 0f;
    private bool startTime = false;
    private GameObject rotator;
    private RuneController runeController;
    private bool tracking = false;
    private float tapDownRotation;
    private float startLocalRotation;



    public RunesInputManager()
    {
        FreezeTime();
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

    public override void Start()
    {
        
        rotator = left.transform.Find("Wrist").Find("Runes(Clone)").Find("Rotator").gameObject;
        runeController = left.transform.Find("Wrist").Find("Runes(Clone)").GetComponent<RuneController>();
    }

    public override void Update()
    {

        rotator = left.transform.Find("Wrist").Find("Runes(Clone)").Find("Rotator").gameObject;
        runeController = left.transform.Find("Wrist").Find("Runes(Clone)").GetComponent<RuneController>();
        //if (tap.GetStateDown(SteamVR_Input_Sources.LeftHand))
        //{

        //    float x = pos.GetAxis(SteamVR_Input_Sources.LeftHand).x;
        //    rotator.transform.Rotate(0, 0, (x / Mathf.Abs(x)) * 45);
        //}
        TrackpadRotation();

        string word = runeController.GetSelected().name;
        bool ready = runeController.GetSpellReady();
        if (grab.GetState(SteamVR_Input_Sources.LeftHand) && word != null && ready)
        {
            if(runeController != null)
                runeController.SetCooldown(word, 0f);
            //GameObject.Find("Globals").GetComponent<Globals>().rotationSave = rotator.transform.localRotation.z;
            Time.timeScale = 1f;
            startTime = false;
            startingTime = 0f;
            Disable();
            switch (word)
            {
                case "Arrow":
                    GameObject.Find("WeaponsController").gameObject.GetComponent<WeaponsController>().currentWeapon = WeaponsController.Weapons.Bow;
                    break;
                case "Ice":
                    GameObject.Find("WeaponsController").gameObject.GetComponent<WeaponsController>().currentWeapon = WeaponsController.Weapons.Ice;
                    break;
                case "Sword":
                    GameObject.Find("WeaponsController").gameObject.GetComponent<WeaponsController>().currentWeapon = WeaponsController.Weapons.Sword;
                    break;
                case "Fire":
                    GameObject.Find("WeaponsController").gameObject.GetComponent<WeaponsController>().currentWeapon = WeaponsController.Weapons.Fire;
                    break;
                case "Lightning":
                    GameObject.Find("WeaponsController").gameObject.GetComponent<WeaponsController>().currentWeapon = WeaponsController.Weapons.Lightning;
                    break;
                case "Air":
                    GameObject.Find("WeaponsController").gameObject.GetComponent<WeaponsController>().currentWeapon = WeaponsController.Weapons.Air;
                    break;

            }
            
        }
        if (startTime)
        {
            Time.timeScale = Mathf.Lerp(0.1f, 1f, (Time.realtimeSinceStartup - startingTime)/duration) ;
        }
        if(Time.realtimeSinceStartup - startingTime > duration)
        {
            startTime = false;
            Time.timeScale = 1f;
        }
    }
    
    public void Disable()
    {
        runeController.Disable();
    }

    public void Enable()
    {
        FreezeTime();
        runeController.Enable();
    }
    private void FreezeTime()
    {
        if (!startTime)
        {
            startingTime = Time.realtimeSinceStartup;
            Time.timeScale = 0.1f;
            startTime = true;
        }
    }
    private void TrackpadRotation()
    {
        Vector2 xy = pos.GetAxis(SteamVR_Input_Sources.LeftHand);
        float slope = xy.y / xy.x;
        float theta = Mathf.Atan(slope);
        if (xy.x < 0)
            theta += Mathf.PI;
        if (xy.x > 0 && xy.y < 0)
            theta += Mathf.PI * 2;

        if (tap.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            tracking = true;
            tapDownRotation = theta;
            startLocalRotation = rotator.transform.localRotation.eulerAngles.z * Mathf.Deg2Rad;
        }
        if (tap.GetStateUp(SteamVR_Input_Sources.LeftHand))
        {
            tracking = false;
            startLocalRotation = rotator.transform.localRotation.eulerAngles.z * Mathf.Deg2Rad;

        }
        if (tracking)
        {
            float changerot = theta - tapDownRotation;
            float truerot = startLocalRotation * Mathf.Rad2Deg + changerot * Mathf.Rad2Deg;
            float nearestRotation = nearestRot(truerot);
            GameObject.Find("Console").GetComponent<Console>().Print("truerot: " + truerot + " nearesteRotation" + nearestRotation);

            rotator.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, nearestRotation));
        }
    }

    private float nearestRot(float rot)
    {
        float bottomBound = (int)(rot /60)*60;
        float topBound = bottomBound + 60;
        GameObject.Find("Console").GetComponent<Console>().Print("bot: " + bottomBound+ " top" + topBound);
        float bottomPredicate = Mathf.Abs(rot - bottomBound);
        float topPredicate = Mathf.Abs(rot - topBound);

        if(bottomPredicate < topPredicate)
        {
            return bottomBound;
        } else
        {
            return topBound;
        }
    }
}
