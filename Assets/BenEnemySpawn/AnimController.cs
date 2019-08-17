using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimController : MonoBehaviour
{
    public NavMeshAgent speedAgent;
    private Transform parentTransform;
    public Animator anim;
    public float animationSpeed = 1f;
    float agentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agentVelocity = speedAgent.velocity.magnitude;
        anim = gameObject.GetComponent<Animator>();
        anim.SetFloat("Speed", agentVelocity * animationSpeed);


        //}



    }
}
