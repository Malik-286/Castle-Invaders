using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth = 100;
     void Start()
    {
        currentHealth = maxHealth;
    }

     void Update()
    {
        
    }
}
