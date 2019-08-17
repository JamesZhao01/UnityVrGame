using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPCMoveSimple : MonoBehaviour
{
    public Material rootMat;
    public Material defaultMat;
    public bool isRooted = false;
    public float rootDuration = 0f;
    public Spawning spawning;

    [SerializeField]
    public Transform agentDestination;

    [SerializeField]
    public NavMeshAgent agent;

    [SerializeField]
    private float agentVelocity = 5f;

    [SerializeField]
    private Transform enemy2;

    [SerializeField]
    private GameObject GOarrow;


    private NavMeshHit hit;
    private bool blocked;
    private bool stopWait = false;
    public bool stopped = false;
    private bool startCounter = false;

    [SerializeField]
    private float arrowTime = 5f;
    private float arrowCounter = 5f;

    [SerializeField]
    private float arrowSpeedMultiplier = 1f;

    private float arrowDelay = 0f;


    void Start()
    {
        agentDestination = GameObject.Find("Destination").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = agentVelocity;

        if (this.tag == "enemy1")
        {
            toObject();
        }

        if (this.tag == "enemy2") 
        {
            stopWait = true;
        }
        
        else
        {
            toObject();
        }



    }

    void Update()
    {
        if (isRooted)
        {
            agent.speed = 0;
            rootDuration -= Time.deltaTime;
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = rootMat;
            Debug.Log("isRooted");
        }
        else
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = defaultMat;
        if (startCounter)
        {
            arrowCounter = arrowCounter + Time.deltaTime;
        }

        arrowDelay = arrowDelay + Time.deltaTime;

        if (stopWait)
        {
            blocked = NavMesh.Raycast(agent.transform.position, agentDestination.position, out hit, NavMesh.AllAreas);
            agent = this.GetComponent<NavMeshAgent>();
            if (blocked)
            {
                toObject();
            }
            else
            {
                toObject();
                agent.stoppingDistance = 10;

                if (agent.velocity == new Vector3(0,0,0))
                {
                    if (arrowDelay > 0.1f)
                    {
                        if (System.Math.Abs(arrowCounter - arrowTime) < 0.3)
                        {
                            GameObject arrow = Instantiate(GOarrow, new Vector3(enemy2.position.x, GOarrow.transform.position.y, enemy2.position.z), GOarrow.transform.rotation);
                            arrow.GetComponent<ArrowPath>().spawning = spawning;
                            arrow.SetActive(true); //b/c the models are not set active initially

                            startCounter = true;
                            arrowCounter = 0;
                            stopped = true;
                        }

                    }



                }   

            }


        }

        CheckCC();
    }

    private void CheckCC()
    {
        if(isRooted && rootDuration < 0)
        {
            isRooted = false;
        }
    }
    public void Root(float duration)
    {
        isRooted = true;
        rootDuration = duration;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("PlayerHitbox"))
            spawning.DestroyEntity(gameObject);
    }

    private void toObject()
    {
        Vector3 directionVector = agentDestination.position;
        agent.SetDestination(directionVector);
    }
}
