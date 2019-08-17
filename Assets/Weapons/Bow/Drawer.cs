using UnityEngine;

public class Drawer : MonoBehaviour
{
    public float prog = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("Draw", prog);
    }
}
