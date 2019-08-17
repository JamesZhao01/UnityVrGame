using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStringController : MonoBehaviour
{
    public GameObject c1;
    public GameObject c2;
    public GameObject o1;
    public GameObject o2;
    // Start is called before the first frame update
    void Start()
    {
        ResetString();
    }

    public void UpdateString(Vector3 stringPos)
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        Vector3 p1 = c1.transform.position;
        Vector3 p3 = c2.transform.position;
        Vector3 p2 = stringPos;

        lr.SetPositions(new Vector3[] {p1, p2, p3});

    }

    public void ResetString()
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        Vector3 p1 = o1.transform.position;
        Vector3 p3 = o2.transform.position;
        Vector3 p2 = Vector3.Lerp(p1, p3, 0.5f);
        lr.SetPositions(new Vector3[] { p1, p2, p3 });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
