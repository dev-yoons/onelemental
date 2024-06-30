using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private Animator animator;
    private bool isDestroyed = false; 
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    { 
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
         
        if (stateInfo.normalizedTime >= 1.0f && !animator.IsInTransition(0) && !isDestroyed)
        { 
            Destroy(gameObject);
            isDestroyed = true;
        }
    }
}
