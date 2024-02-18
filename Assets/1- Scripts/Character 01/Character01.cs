using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character01 : MonoBehaviour
{

    Animator animator;
     void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("isRunning");
    }

    
}
