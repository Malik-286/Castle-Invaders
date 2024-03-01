using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int currentHealthPoints;
    [SerializeField] int maxHealthPoints = 3;
    [SerializeField] int difficultyRamp = 1;
 

    CurrencyManager currencyManager;
    GamePlayUI gamePlayUI;
    void OnEnable()
    {
        currentHealthPoints = maxHealthPoints;
    }
    void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
        gamePlayUI = FindObjectOfType<GamePlayUI>();

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
            gamePlayUI.enemiesKillsCout++;
            Destroy(gameObject);
        }
         
    }

    void OnParticleCollision(GameObject other)
    {       
            ProcessHit();        
    }

    
}
