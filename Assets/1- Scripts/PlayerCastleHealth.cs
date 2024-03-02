using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastleHealth : MonoBehaviour
{

    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth = 100;
    [SerializeField] bool bIsDestroyed = false;
    [SerializeField] GameObject  destroyParticles;
      
     GameObject[] enemiesArray;
     GameObject[] enemiesPool;

 
    Animator animator;
    GamePlayUI gamePlayUI;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        animator.enabled = false;
        gamePlayUI = FindObjectOfType<GamePlayUI>();

    }

    public int GetCurretHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

     void Update()
    {
        CheckPlayerHealht();
    }

    public void LoseHealth(int amountToLose)
    {
        currentHealth -= amountToLose;
     }


    

    void CheckPlayerHealht()
    {
        if (currentHealth <= 0 && bIsDestroyed == false)
        {
            gamePlayUI.DisableTowersPanel();
            DestroyAllEnemies();
            animator.enabled = true;
            bIsDestroyed = true;
            animator.SetTrigger("Destroy");
            destroyParticles.SetActive(true);
            Debug.Log("Castle Destroyed..!");
            gamePlayUI.ActivateDeathPanel();
 
        }
    }

    void DestroyAllEnemies()
    {

         GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

         GameObject[] pools = GameObject.FindGameObjectsWithTag("EnemiesPool");

         foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

         foreach (GameObject pool in pools)
        {
            Destroy(pool);
        }
    }

    

     
}




