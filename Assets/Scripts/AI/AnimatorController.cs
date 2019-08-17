using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorController : MonoBehaviour
{
    public bool updateString;
    public GameObject hand;
    public GameObject stringObject;
    public NavMeshAgent speedAgent;
    private Transform parentTransform;
    public Animator anim;
    public float animationSpeed = 1f;
    float agentVelocity;
    private bool isIdle = false;
    public bool idle
    {
        get
        {
            return isIdle;
        }
        set
        {
            isIdle = value;
            handleIdle();
        }
    }
    public bool isAttacking = false;
    public bool justAttacked = false;

    public bool isFiring = false;
    // Start is called before the first frame update
    public void handleIdle()
    {

    }

    public void AttackReady()
    {

        if (justAttacked)
        {
            justAttacked = false;
            isAttacking = false;
        }
    }
    public void AttackDone()
    {

        justAttacked = true;
        anim.SetBool("attack2", false);
    }
    public void Attack()
    {
        if (!isAttacking && !justAttacked)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("attack2", true);
            isAttacking = true;
        }

    }

    public void DrawBow()
    {
        if (!isFiring)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("drawBow", true);
            isFiring = true;
        }

    }

    public void BowDrawn()
    {
        updateString = true;

    }

    public void FireArrow()
    {
        updateString = false;
        isFiring = false;
        stringObject.GetComponent<EnemyStringController>().ResetString();
        gameObject.transform.parent.parent.gameObject.GetComponent<AIArcher>().ShootArrow();
        anim.SetBool("drawBow", false);
    }

    public void Run()
    {
        if (!isAttacking)
        {
            anim.SetBool("isRunning", true);
        }
    }

    public void ArcherRun()
    {
        if(!isFiring)
        {
            anim.SetBool("isRunning", true);
        }
    }
    void Start()
    {
        agentVelocity = speedAgent.velocity.magnitude;
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("isRunning", true);
    }

    public void EnterImplosion()
    {
        isAttacking = false;
        isFiring = false;
        anim.SetBool("attack2", false);
        anim.SetBool("drawBow", false);
        anim.SetBool("isRunning", false);
    }
    // Update is called once per frame
    void Update()
    {
        if(stringObject != null)
        {
            if (updateString)
                stringObject.GetComponent<EnemyStringController>().UpdateString(hand.transform.position);
            else
                stringObject.GetComponent<EnemyStringController>().ResetString();
        }
    }
}
