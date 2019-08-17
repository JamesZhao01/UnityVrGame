using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIArcher : AI
{
    public bool debug = false;
    private bool fire = false;
    public float timeSinceArrow = 0f;
    public float arrowDelay;
    public GameObject arrowObject;
    private NavMeshHit hit;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agentDestination = GameObject.Find("Destination");
        animatorController = gameObject.GetComponentInChildren<AnimatorController>();
    }
    public override void AIMove()
    {
        if (implode || !agent.enabled)
            return;
        //if (GetComponent<NavMeshAgent>().enabled)
        //{
        //    if (agentSpeedOverride == -1)
        //        agent.speed = agentVelocity;
        //    else
        //        agent.speed = agentSpeedOverride;
        //    agent.SetDestination(agentDestination.transform.position);
        //}
        agent.speed = agentVelocity;
        if(agent.remainingDistance > agent.stoppingDistance)
            animatorController.ArcherRun();
        //if (isBlocked())
        //{
        //    agent.stoppingDistance = 0f;
        //}
        //else
        //{
        //    agent.stoppingDistance = 10f;
        //}
    }

    private bool isBlocked()
    {
        bool blocked = NavMesh.Raycast(agent.transform.position, agentDestination.transform.position, out hit, NavMesh.AllAreas);
        return blocked;
    }

    public override void AIAttack()
    {
        timeSinceArrow += Time.deltaTime;
        if (implode || !agent.enabled)
            return;
        if (animatorController.isFiring)
        {
            agent.speed = 0f;
            agent.angularSpeed = 0f;
        } else if(agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 position = agentDestination.transform.position;
            Vector3 agentPos = gameObject.transform.position;
            Vector3 rotation = (gameObject.transform.rotation * Vector3.forward);
            rotation = new Vector3(rotation.x, 0, rotation.z).normalized;
            Vector3 displacement = (position - agentPos);
            displacement = new Vector3(displacement.x, 0, displacement.z).normalized;
            float angle = Vector3.Angle(rotation, displacement);
            if (angle >= 5)
            {
                gameObject.transform.rotation = Quaternion.LookRotation(Vector3.Lerp(rotation, displacement, Time.deltaTime * 8), Vector3.up);
            }
            else
            {
                agent.speed = 0f;
                if (timeSinceArrow >= arrowDelay && GetComponentInChildren<Animator>().GetBool("drawBow") == false && !animatorController.isFiring)
                {
                    animatorController.DrawBow();
                    timeSinceArrow = 0f;
                }
            }
        }
    }

    public void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.5f, gameObject.transform.position.z), arrowObject.transform.rotation);
        arrow.GetComponent<Rigidbody>().position = arrow.transform.position;
        arrow.GetComponent<ArrowPath>().CalculateValues();
        arrow.SetActive(true);
    }

    public override void ImplementedUpdate()
    {
        if (gameObject.GetComponent<NavMeshAgent>().enabled)
            SetTarget();
        if (implode)
        {
            Debug.Log("flyin");
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
}
