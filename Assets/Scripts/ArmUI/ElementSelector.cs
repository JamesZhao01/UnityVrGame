using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSelector : MonoBehaviour
{
    public GameObject[] runes;
    public GameObject publicselected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject selected = null;
        foreach(GameObject rune in runes)
        {
            if (selected == null)
            {
                selected = rune;
                continue;
            }
            if (rune.transform.position.y > selected.transform.position.y)
                selected = rune;
        }
        selected.transform.localScale = Vector3.Lerp(selected.transform.localScale, new Vector3(2, 2, 2), 4 * Time.deltaTime);
        foreach (GameObject rune in runes)
        {
            if (rune != selected)
            {
                rune.transform.localScale = new Vector3(1,1, 1);
            }
        }
        publicselected = selected;
    }
}
