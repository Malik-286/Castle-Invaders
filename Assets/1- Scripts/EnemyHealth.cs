using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int currentHealthPoints;
    [SerializeField] int difficultyRamp = 1;
    [SerializeField] int maxHealthPoints;
    [SerializeField] GameObject deathParticles;



    CurrencyManager currencyManager;
    GamePlayUI gamePlayUI;
    PlayerCastleHealth playerHealth;

    bool isDeathCountIncreased = false;

    void OnEnable()
    {
        currentHealthPoints = maxHealthPoints;
    }

   
    void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
        gamePlayUI = FindObjectOfType<GamePlayUI>();
        playerHealth = FindObjectOfType<PlayerCastleHealth>();
        deathParticles.SetActive(false);
 
    }


  
    void ProcessHit()
    {
        currentHealthPoints--;

        if (currentHealthPoints <= 0)
        {
            maxHealthPoints += difficultyRamp;
            
            currencyManager.IncreaseGold(5);
            deathParticles.SetActive(true);
            if (isDeathCountIncreased == false)
            {
                gamePlayUI.enemiesKillsCout++;
                isDeathCountIncreased = true;
            }
            Destroy(gameObject, 0.1f);
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
