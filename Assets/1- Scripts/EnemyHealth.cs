using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth = 3;

    void Start()
    {
        currentHealth = maxHealth;
    }

     void Update()
    {
        
    }

    

    void ProcessHit()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
         
    }

    void OnParticleCollision(GameObject other)
    {       
            ProcessHit();        
    }
}
