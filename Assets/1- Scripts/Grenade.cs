using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    [SerializeField] float delay = 5f;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float radius;

    float countDown;
 

    bool hasExploded = false;
     void Start()
    {
        countDown = delay;
    }

     void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0 && hasExploded == false) 
        {
            Expolde();
        }
     }

    void Expolde()
    {

       GameObject gernadeClone = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        
        hasExploded = true;
        Destroy(gernadeClone, 0.9f);
        Destroy(gameObject);
    }
}
