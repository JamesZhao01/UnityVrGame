using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;

public class ArrowPath : MonoBehaviour
{
    public Vector3 prevPos;
    public float speed = 0.1f;
    public Spawning spawning;
    public Vector3 targetPos;
    public Vector3 startPos;
    public Vector3 direction;

    public Vector3 apex;

    public Transform lookHere;

    // THIS IS NOT A VECTOR. x y z = a b c 

    private float percentage = 0f;
    private Vector3 polynomial;
    private float range;
    private float maxX;


    void Start()
    {

    }
    public void CalculateValues()
    {
        targetPos = GameObject.Find("Destination").transform.position;
        startPos = gameObject.transform.position;


        Vector3 directionVector = targetPos - startPos;
        Vector3 midpoint = Vector3.Lerp(startPos, targetPos, 0.5f);
        Vector3 horizontal = -Vector3.Cross(directionVector, Vector3.up);
        Vector3 additiveApex = Vector3.Cross(directionVector, horizontal).normalized * directionVector.magnitude / 8;
        apex = midpoint + additiveApex;

        //Vector3 projectedStart = new Vector3(startPos.x, 0, startPos.z);
        //Vector3 projectedTarget = new Vector3(targetPos.x, 0, targetPos.z);

        //range = (projectedTarget - projectedStart).magnitude;

        //Vector2 startPos2D = new Vector3(new Vector2(startPos.x, startPos.z).magnitude, startPos.y);
        //Vector2 targetPos2D = new Vector3(new Vector2(targetPos.x, targetPos.z).magnitude, targetPos.y);
        //Vector2 apex2D = new Vector3(new Vector2(apex.x, apex.z).magnitude, apex.y);
        //polynomial = GenerateLagrange(startPos2D, targetPos2D, apex2D);
        //Debug.Log(polynomial);
        //maxX = Mathf.Max(new Vector2(startPos.x, startPos.z).magnitude, new Vector2(targetPos.x, targetPos.z).magnitude, new Vector2(apex.x, apex.z).magnitude);

        Vector3 relativeStart = Vector3.zero;
        Vector3 relativeApex = apex - startPos;
        Vector3 relativeTarget = targetPos - startPos;

        Vector3 relativeStartProj = new Vector3(relativeStart.x, 0, relativeStart.z);
        Vector3 relativeApexProj = new Vector3(relativeApex.x, 0, relativeApex.z);
        Vector3 relativeTargetProj = new Vector3(relativeTarget.x, 0, relativeTarget.z);
    
        Vector2 relativeStartLR = Vector2.zero;
        Vector2 relativeApexLR = new Vector2(relativeApexProj.magnitude, relativeApex.y);
        Vector2 relativeTargetLR = new Vector2(relativeTargetProj.magnitude, relativeTarget.y);

        polynomial = GenerateLagrange(relativeStartLR, relativeApexLR, relativeTargetLR);
    }

    private float Interpolate(Vector3 polynomialVector, Vector3 startPos, Vector3 endPos, float percentage)
    {

        Vector3 relativeTarget = targetPos - startPos;
        Vector3 relativeTargetProj = new Vector3(relativeTarget.x, 0, relativeTarget.z);
        Vector2 relativeTargetLR = new Vector2(relativeTargetProj.magnitude, relativeTarget.y);

        float x = Mathf.Lerp(0, relativeTargetProj.magnitude, percentage);

        return polynomialVector.x * x * x + polynomialVector.y * x + polynomialVector.z;
    }

    private Vector3 GenerateLagrange(Vector2 p0, Vector2 p1, Vector2 p2)
    {
        // n = y_i / (shit in denominator)
        float n0 = p0.y / ((p0.x - p1.x) * (p0.x - p2.x));
        float n1 = p1.y / ((p1.x - p0.x) * (p1.x - p2.x));
        float n2 = p2.y / ((p2.x - p1.x) * (p2.x - p0.x));

        float a = n0 + n1 + n2;
        float b = -n0 * (p1.x + p2.x) - n1 * (p0.x + p2.x) - n2 * (p0.x + p1.x);
        float c = n0 * p1.x * p2.x + n1 * p0.x * p2.x + n2 * p0.x * p1.x;

        // ax^2 + bx + c form
        return new Vector3(a, b, c);
    }

    void Update()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        prevPos = rb.transform.position;
        percentage += Time.deltaTime * speed * 1f;

        if(percentage >= 1)
        {
            Destroy(gameObject);
        }

        Vector2 startPosXZ = new Vector2(startPos.x, startPos.z);
        Vector2 targetPosXZ = new Vector2(targetPos.x, targetPos.z);

        Vector2 pos = Vector2.Lerp(startPosXZ, targetPosXZ, percentage);
        float height = Interpolate(polynomial, startPos, targetPos, percentage) + startPos.y;

        Vector3 position = new Vector3(pos.x, height, pos.y);
        Vector3 angle = position - prevPos;
        direction = angle;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, angle);
        rb.rotation = rotation;
        //gameObject.transform.rotation = rotation;
        rb.position = position;



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("PlayerHitbox"))
        {
            //GameObject.Find("SceneController").GetComponent<SceneHandler>().Dead();
        }
        if (other.tag == "Finish")
        {
            Destroy(gameObject);
        }

    }



}