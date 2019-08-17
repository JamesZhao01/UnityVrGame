using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public LineRenderer l;
    public Camera c;
    // Start is called before the first frame update
    void Awake()
    {
        MeshCollider m = gameObject.AddComponent<MeshCollider>();
        Mesh mesh = new Mesh();
        l.BakeMesh(mesh, c, true);
        m.sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetString()
    {
        Vector3[] points = new Vector3[3] { new Vector3(0, 0.85f, 0), new Vector3(0, 0, 0), new Vector3(0, -0.85f, 0)};
        l.SetPositions(points);
    }
}
