using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneEquipper : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    // Start is called before the first frame update
    void Start()
    {
        leftHand.GetComponent<HandController>().animator.SetBool("IsHolding", true);
        Debug.Log("Bow");
        leftHand.GetComponent<HandController>().UnEquip();
        rightHand.GetComponent<HandController>().UnEquip();

        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bow.prefab", typeof(GameObject));
        GameObject newObject = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
        leftHand.GetComponent<HandController>().Equip(newObject, "BowHolder");
        BowInputManager bim = new BowInputManager();
        bim.isInfinite = true;
        bim.bowData = newObject.GetComponentInChildren<BowData>();

        leftHand.GetComponent<HandController>().inputManager = bim;
        rightHand.GetComponent<HandController>().inputManager = bim;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
