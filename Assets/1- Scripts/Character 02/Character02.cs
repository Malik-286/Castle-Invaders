using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character02 : MonoBehaviour
{

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Run", true);
 
    }

 


}
