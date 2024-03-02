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
    PlayerCastleHealth playerHealth;
    void OnEnable()
    {
        currentHealthPoints = maxHealthPoints;
    }
    void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
        gamePlayUI = FindObjectOfType<GamePlayUI>();
        playerHealth = FindObjectOfType<PlayerCastleHealth>();

    }

    
    

    void ProcessHit()
    {
        currentHealthPoints--;

        if (currentHealthPoints <= 0)
        {
            maxHealthPoints += difficultyRamp;
            gamePlayUI.enemiesKillsCout++;
            currencyManager.IncreaseGold(5);
            Destroy(gameObject);
        }
         
    }

    void OnParticleCollision(GameObject other)
    {       
            ProcessHit();        
    }

      void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerCastle"))
        {
            playerHealth.LoseHealth(10);
        }
         
    }


}
