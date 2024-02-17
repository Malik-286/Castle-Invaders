using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int currentHealthPoints;
    [SerializeField] int maxHealthPoints = 3;
    [SerializeField] int difficultyRamp = 1;

    void OnEnable()
    {
        currentHealthPoints = maxHealthPoints;
    }

     void Update()
    {
        
    }

    

    void ProcessHit()
    {
        currentHealthPoints--;

        if (currentHealthPoints <= 0)
        {
            maxHealthPoints += difficultyRamp;
            Destroy(gameObject);
        }
         
    }

    void OnParticleCollision(GameObject other)
    {       
            ProcessHit();        
    }
}
