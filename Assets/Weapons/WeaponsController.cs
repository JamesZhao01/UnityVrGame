using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
public class WeaponsController : MonoBehaviour
{
    private bool firstTimeEquippingRunes = true;
    public GameObject leftHand;
    public GameObject rightHand;
    public enum Weapons{
        None = 0, Bow = 1, Fire = 2, Ice = 3, Sword = 4, Lightning = 5, Air = 6
    }
    [SerializeField]
    private Weapons weaponsId;
    public Weapons currentWeapon {
        get
        {
            return weaponsId;
        }
        set
        {
            weaponsId = value;
            SwitchWeapon();
        }
    }

    
    void Start()
    {
        currentWeapon = Weapons.None;
    }
    void Update()
    {
 
        if (Input.GetKeyDown("r"))
        {
            currentWeapon = Weapons.Bow;
        }
        if (Input.GetKeyDown("u"))
        {
            currentWeapon = Weapons.None;
        }
    }
    private void SwitchWeapon()
    {
        DefaultInputManager dim = new DefaultInputManager();
        switch (weaponsId)
        {

            case Weapons.None:
                Debug.Log("None");
                leftHand.GetComponent<HandController>().UnEquip();
                rightHand.GetComponent<HandController>().UnEquip();
                leftHand.GetComponent<HandController>().inputManager = dim;
                rightHand.GetComponent<HandController>().inputManager = dim;
                if (firstTimeEquippingRunes)
                {
                    Object runes = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Runes.prefab", typeof(GameObject));
                    GameObject runesObject = (GameObject)Instantiate(runes, Vector3.zero, Quaternion.identity);
                    //leftHand.GetComponent<HandController>().Equip(runesObject, "Wrist");

                    RunesInputManager rim = new RunesInputManager();
                    leftHand.GetComponent<HandController>().EquipRunes(runesObject);
                    leftHand.GetComponent<HandController>().EquipRuneManager(rim);
                    firstTimeEquippingRunes = false;
                } else
                {
                    leftHand.GetComponent<HandController>().rim.Enable();
                }
                break;
            case Weapons.Bow:
                leftHand.GetComponent<HandController>().animator.SetBool("IsHolding", true);
                Debug.Log("Bow");
                leftHand.GetComponent<HandController>().UnEquip();
                rightHand.GetComponent<HandController>().UnEquip();

                Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bow.prefab", typeof(GameObject));
                GameObject newObject = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
                leftHand.GetComponent<HandController>().Equip(newObject, "BowHolder");
                BowInputManager bim = new BowInputManager();
                bim.bowData = newObject.GetComponentInChildren<BowData>();
                leftHand.GetComponent<HandController>().inputManager = bim;
                rightHand.GetComponent<HandController>().inputManager = bim;
                break;
            case Weapons.Ice:
                leftHand.GetComponent<HandController>().UnEquip();
                rightHand.GetComponent<HandController>().UnEquip();

                //Object wand = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Wand.prefab", typeof(GameObject));
                //GameObject wandObject = (GameObject)Instantiate(wand, Vector3.zero, Quaternion.identity);
                //rightHand.GetComponent<HandController>().Equip(wandObject, "WandHolder");

                Object frostBolt = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/FrostBolt.prefab", typeof(GameObject));
                GameObject frostBoltObject = (GameObject)Instantiate(frostBolt, Vector3.zero, Quaternion.identity);

                SpellCaster wim = new SpellCaster();
                wim.castableObject = frostBoltObject;

                rightHand.GetComponent<HandController>().inputManager = wim;
                break;
            case Weapons.Sword:
                leftHand.GetComponent<HandController>().UnEquip();
                leftHand.GetComponent<HandController>().rim.Disable();
                rightHand.GetComponent<HandController>().UnEquip();

                Object sword = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Sword.prefab", typeof(GameObject));
                GameObject swordObject = (GameObject)Instantiate(sword, Vector3.zero, Quaternion.identity);

                Object shield = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Shield.prefab", typeof(GameObject));
                GameObject shieldObject = (GameObject)Instantiate(shield, Vector3.zero, Quaternion.identity);

                DefaultInputManager defIM = new DefaultInputManager();
                SwordInputManager sim = new SwordInputManager();
                rightHand.GetComponent<HandController>().inputManager = defIM;
                leftHand.GetComponent<HandController>().inputManager = sim;
                rightHand.GetComponent<HandController>().Equip(swordObject, "WandHolder");
                leftHand.GetComponent<HandController>().Equip(shieldObject, "Wrist");
                break;

            case Weapons.Fire:
                leftHand.GetComponent<HandController>().UnEquip();
                rightHand.GetComponent<HandController>().UnEquip();

                //Object wand = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Wand.prefab", typeof(GameObject));
                //GameObject wandObject = (GameObject)Instantiate(wand, Vector3.zero, Quaternion.identity);
                //rightHand.GetComponent<HandController>().Equip(wandObject, "WandHolder");

                GameObject meteor = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Weapons/Meteor/Meteor.prefab", typeof(GameObject));
                LobCaster lc = new LobCaster();
                lc.lobbable = meteor;

                Debug.Log(meteor);
                leftHand.GetComponent<HandController>().inputManager = dim;
                rightHand.GetComponent<HandController>().inputManager = lc;
                break;

            case Weapons.Lightning:
                leftHand.GetComponent<HandController>().UnEquip();
                rightHand.GetComponent<HandController>().UnEquip();

                GameObject bolt = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Lightning.prefab", typeof(GameObject));
                GameObject boltObj = Instantiate(bolt);
                rightHand.GetComponent<HandController>().Equip(boltObj, "LightningSource");
                boltObj.transform.localPosition = Vector3.zero;
                boltObj.transform.localRotation = Quaternion.identity;
                LightningInputManager lim = new LightningInputManager();

                leftHand.GetComponent<HandController>().inputManager = dim;
                rightHand.GetComponent<HandController>().inputManager = lim;
                break;
            case Weapons.Air:
                leftHand.GetComponent<HandController>().UnEquip();
                rightHand.GetComponent<HandController>().UnEquip();

                //Object wand = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Wand.prefab", typeof(GameObject));
                //GameObject wandObject = (GameObject)Instantiate(wand, Vector3.zero, Quaternion.identity);
                //rightHand.GetComponent<HandController>().Equip(wandObject, "WandHolder");

                GameObject airPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/AirAttack.prefab", typeof(GameObject));
                LobCaster airLc = new LobCaster();
                airLc.lobbable = airPrefab;

                leftHand.GetComponent<HandController>().inputManager = dim;
                rightHand.GetComponent<HandController>().inputManager = airLc;
                break;

            default:
                Debug.Log("Nonexistent Id");
                break;

        }
    }
}
