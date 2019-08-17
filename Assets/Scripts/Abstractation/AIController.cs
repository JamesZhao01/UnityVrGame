using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Material mat1;
    public Material mat2;
    AI ai;
    [SerializeField]
    float health = 1.0f;
    public float healDuration = 0f;
    protected float rootDuration = 0f;
    protected bool isRooted = false;
    private Animator animator;

    private void Start()
    {
        ai = gameObject.GetComponent<AI>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    public void Damage(float f)
    {
        health -= f;
        if(health < 0)
        {
            GameObject.Find("Terrain").GetComponent<Spawning>().DestroyEntity(gameObject);
        }
    }
    public void Root(float duration)
    {
        isRooted = true;
        rootDuration = duration;
    }

    public void UpdateRoot()
    {
        if (isRooted) {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = mat1;
            gameObject.GetComponent<NavMeshAgent>().speed = 0f;
            animator.speed = 0f;
            rootDuration -= Time.deltaTime;
            if (rootDuration < 0)
            {
                isRooted = false;
                rootDuration = 0;
            }
        } else
        {
            //gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = mat2;
            animator.speed = 1f;
        }
    }
    void Update()
    {
        ai.ImplementedUpdate();
        ai = gameObject.GetComponent<AI>();
        ai.AIAttack();
        ai.AIMove();
        UpdateRoot();

    }
    

}
