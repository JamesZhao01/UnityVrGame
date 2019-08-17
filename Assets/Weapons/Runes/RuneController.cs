using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneController : MonoBehaviour
{
    Globals globals;
    GameObject selected = null;
    GameObject selectedLocator = null;
    public Dictionary<string, float> cooldowns = new Dictionary<string, float>() { { "Arrow",0 }, { "Sword", 0 }, { "Shield", 0 }, { "Fire", 0 }, { "Ice", 0 }, { "Air", 0 }, { "Time", 0 }, { "Lightning", 0 } };

    void Start()
    {
        globals = GameObject.Find("Globals").GetComponent<Globals>();
    }
    // Update is called once per frame
    void Update()
    {
        selected = null;
        GameObject reference = gameObject.transform.Find("Reference").gameObject;
        GameObject rotator = gameObject.transform.Find("Rotator").gameObject;
        for(int i = 0; i < rotator.transform.childCount; i++)
        {
            GameObject rune = rotator.transform.GetChild(i).gameObject;
            string name = rune.name;
            GameObject locator = rune.transform.Find("Rune").gameObject;
            GameObject ring = locator.transform.Find("Ring").gameObject;

            cooldowns[name] = cooldowns[name] + Time.deltaTime;
            cooldowns[name] = Mathf.Clamp(cooldowns[name], 0f, globals.cooldowns[name]);
            float percentage = cooldowns[name] / globals.cooldowns[name];
            ring.GetComponent<MeshRenderer>().material.SetFloat("_Percent", percentage);


            if (selected == null)
            {
                selected = rune;
                selectedLocator = locator;
                ring.GetComponent<MeshRenderer>().material.SetFloat("_Selected", 1);
            }
            if(reference.transform.InverseTransformPoint(locator.transform.position).y > reference.transform.InverseTransformPoint(selectedLocator.transform.position).y)
            {
                selectedLocator.transform.Find("Ring").GetComponent<MeshRenderer>().material.SetFloat("_Selected", 0);
                selected = rune;
                selectedLocator = locator;
                ring.GetComponent<MeshRenderer>().material.SetFloat("_Selected", 1);
            }
        }

        for(int i = 0; i < rotator.transform.childCount; i++)
        {
            GameObject rune = rotator.transform.GetChild(i).Find("Rune").gameObject;
            if(rune != selectedLocator)
            {
                rune.transform.localScale = Vector3.Lerp(rune.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 4);
                rune.transform.Find("Ring").GetComponent<MeshRenderer>().material.SetFloat("_Selected", 0);
            }
        }
        selectedLocator.transform.localScale = Vector3.Lerp(selectedLocator.transform.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime * 4);

    }

    public GameObject GetSelected()
    {
        return selected;
    }

    public bool GetSpellReady()
    {
        return cooldowns[selected.name] >= globals.cooldowns[selected.name];
    }

    public void SetCooldown(string name, float value)
    {
        cooldowns[name] = value;
    }

    public void Disable()
    {
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }
    }

    public void Enable()
    {
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = true;
        }
    }
}
