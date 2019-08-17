using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class LightningControllerNew : MonoBehaviour
{
    Particle[] particles = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer lr = GetComponentInChildren<LineRenderer>();
        Vector3 p0 = gameObject.transform.position;
        Vector3 p1 = p0 + (gameObject.transform.rotation * Vector3.forward).normalized * 2;
        Vector3 p2 = p1;
        Vector3 p3 = new Vector3(10, 10, 10);
        for (int i = 0; i < lr.positionCount; i++)
        {
            float t = i / 23f;
            Vector3 pos = Mathf.Pow(1 - t, 3) * p0 + 3 * Mathf.Pow(1 - t, 2) * t * p1 + 3 * (1 - t) * t * t * p2 + Mathf.Pow(t, 3) * p3;
            lr.SetPosition(i, new Vector3(pos.x + Random.Range(-0.2f, 0.2f), pos.y + Random.Range(-0.2f, 0.2f), pos.z + Random.Range(-0.2f, 0.2f)));

        }
    }
}
