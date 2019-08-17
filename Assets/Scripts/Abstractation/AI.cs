using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AI : MonoBehaviour
{
    [SerializeField]
    protected bool implode = false;
    [SerializeField]
    protected bool floating = false;
    protected AnimatorController animatorController;
    protected float timeOfImplode;
    protected Vector3 target;
    protected Vector3 launchLocation;

    public AIController AiController;
    protected float agentVelocity = 1f;
    protected float agentSpeedOverride = -1f;
    public float agentSpeed
    {
        set {agentSpeedOverride = value;}
    }


    protected void SetTarget()
    {
        agentDestination = GameObject.Find("Destination");
        agent.SetDestination(agentDestination.transform.position);
    }

    protected void DeactivateNavmesh()
    {
        floating = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<NavMeshAgent>().enabled = false;
    }
    protected void ActivateNavmesh()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        floating = false;
        GetComponent<NavMeshAgent>().enabled = true;
        SetTarget();
    }
    protected void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Finish"))
        {
            ActivateNavmesh();
        }
    }

    public void Implode(Vector3 epicenter)
    {
        timeOfImplode = Time.realtimeSinceStartup;
        DeactivateNavmesh();
        GetComponent<Rigidbody>().useGravity = false;
        target = epicenter;
        launchLocation = gameObject.transform.position;
        implode = true;
    }
    public GameObject agentDestination;
    protected NavMeshAgent agent;
    public abstract void AIMove();
    public abstract void AIAttack();
    public abstract void ImplementedUpdate();

}
