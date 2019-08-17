using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWarrior : AI
{
    public GameObject navMeshDestination;
    public float navMeshSpeed;


    void Start()
    {
        animatorController = gameObject.GetComponentInChildren<AnimatorController>();
        agent = GetComponent<NavMeshAgent>();
        agentDestination = GameObject.Find("Destination");
        if (GetComponent<NavMeshAgent>().enabled)
        {
            if (agentSpeedOverride == -1)
                agent.speed = agentVelocity;
            else
                agent.speed = agentSpeedOverride;
            agent.SetDestination(agentDestination.transform.position);
        }
    }

    public override void ImplementedUpdate()
    {
        if(gameObject.GetComponent<NavMeshAgent>().enabled)
            SetTarget();
        if (implode)
        {
            animatorController.EnterImplosion();
            //gameObject.GetComponent<Rigidbody>().position - target).magnitude <= 0.5 &&
            gameObject.GetComponent<Rigidbody>().velocity = (target - gameObject.transform.position) * 5 * (target - gameObject.transform.position).magnitude / (target - launchLocation).magnitude;
            if (Time.realtimeSinceStartup - timeOfImplode >= 1.5f)
            {
                GetComponent<Rigidbody>().useGravity = true;
                implode = false;
            }
        }
    }

    public override void AIAttack()
    {
        if (implode || agent.enabled == false)
            return;
        if (animatorController.isAttacking)
        {
            agent.speed = 0f;
            agent.angularSpeed = 0f;
        }
        else
        {
            Vector3 position = agentDestination.transform.position;
            Vector3 agentPos = gameObject.transform.position;
            Vector3 rotation = (gameObject.transform.rotation * Vector3.forward);
            rotation = new Vector3(rotation.x, 0, rotation.z).normalized;
            Vector3 displacement = (position - agentPos);
            displacement = new Vector3(displacement.x, 0, displacement.z).normalized;
            float angle = Vector3.Angle(rotation, displacement);
            if (agent.remainingDistance <= 1f)
            {
                agent.angularSpeed = 0f;
                agent.velocity = Vector3.zero;
                agent.speed = 0f;
                if (angle >= 5)
                {
                    gameObject.transform.rotation = Quaternion.LookRotation(Vector3.Lerp(rotation, displacement, Time.deltaTime * 8), Vector3.up);
                } else
                {
                    gameObject.GetComponentInChildren<AnimatorController>().Attack();
                }
            }
            else
            {
                agent.speed = agentVelocity;
                agent.angularSpeed = 720f;
            }

        }

    }

    public override void AIMove()
    {
        if (implode || agent.enabled == false)
            return;
        agent.speed = agentVelocity;
        animatorController.Run();
    }
}
