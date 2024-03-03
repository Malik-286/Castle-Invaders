using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastleHealth : MonoBehaviour
{

    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth = 100;
    [SerializeField] bool bIsDestroyed = false;
    [SerializeField] GameObject  destroyParticles;
    [SerializeField] GameObject castleWinParticles;
    [SerializeField] GameObject smokeParticles;


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
        castleWinParticles.SetActive(false);
        smokeParticles.SetActive(false);

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
        smokeParticles.SetActive(true);
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
         smokeParticles.SetActive(false);
         gamePlayUI.DisableTowersPanel();
         castleWinParticles.SetActive(true);

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




