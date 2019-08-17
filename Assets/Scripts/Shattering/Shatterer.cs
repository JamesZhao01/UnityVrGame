using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shatterer : MonoBehaviour
{
    public GameObject shatter;
    public GameObject lowPoly;
    public GameObject existingArmature;
    public GameObject shatteredArmature;
    void RecursiveCopy(GameObject existingChild, GameObject shatteredChild)
    {
        if (existingChild.name == shatteredChild.name)
        {
            shatteredChild.transform.localPosition = existingChild.transform.localPosition;
            shatteredChild.transform.localRotation = existingChild.transform.localRotation;
            shatteredChild.transform.localScale = existingChild.transform.localScale;
        }
        for (int i = 0; i < existingChild.transform.childCount; i++)
        {
            for (int j = 0; j < shatteredChild.transform.childCount; j++)
            {
                if(shatteredChild.transform.GetChild(j).name.Contains("cell") && shatteredChild.transform.GetChild(j).gameObject.GetComponent<MeshCollider>() == null)
                {
                    shatteredChild.transform.GetChild(j).gameObject.AddComponent<Rigidbody>();
                    MeshCollider col = shatteredChild.transform.GetChild(j).gameObject.AddComponent<MeshCollider>();
                    col.convex = true;
                    Destroy(shatteredChild.transform.GetChild(j).gameObject, Random.Range(1, 2));

                }
                if (existingChild.transform.GetChild(i).name == shatteredChild.transform.GetChild(j).name)
                {
                    RecursiveCopy(existingChild.transform.GetChild(i).gameObject, shatteredChild.transform.GetChild(j).gameObject);
                }
            }
        }
    }

    void Start()
    {
        
    }

    public void Shatter()
    {
        shatter.SetActive(true);
        lowPoly.SetActive(false);
        RecursiveCopy(existingArmature, shatteredArmature);
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<AI>().enabled = false;
        gameObject.GetComponent<AIController>().enabled = false;
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {

        }
    }
}
