using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    GamePlayUI gamePlayUI;
    ObjectPool[] objectPools;
    LevelUnLocker levelUnlocker;
    GameManager gameManager;
    PlayerCastleHealth playerCastleHealth;



    void Start()
    {
        gamePlayUI = FindObjectOfType<GamePlayUI>();
        objectPools = FindObjectsOfType<ObjectPool>();
        levelUnlocker = FindObjectOfType<LevelUnLocker>();
        gameManager = FindObjectOfType<GameManager>();
        playerCastleHealth = FindObjectOfType<PlayerCastleHealth>();
 
    }


    void Update()
    {
        CheckPlayerWin();
    }


    void CheckPlayerWin()
    {

        if (objectPools != null)
        {
            bool allPoolsEmpty = true;
            bool anyEnemiesAlive = false;

            foreach (ObjectPool pool in objectPools)
            {
                if (pool.GetNumberOfPools() > 0)
                {
                    allPoolsEmpty = false;
                }

                if (CheckIfAnyEnemyisAlive(pool))
                {
                    anyEnemiesAlive = true;
                    break;
                }
            }

            // Check if all pools are empty, no enemies alive, and player's health is greater than zero
            if (allPoolsEmpty && !anyEnemiesAlive && playerCastleHealth.GetCurretHealth() > 0)
            {
                Debug.Log("Player has won the game");
                StartCoroutine(gamePlayUI.ActivateWinPanel());
                
                if (levelUnlocker != null)
                {
                    levelUnlocker.UnlockLevel(gameManager.GetCurrentSceneIndex());
                }
              
            }
        }


    }

    public bool CheckIfAnyEnemyisAlive(ObjectPool pool)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length > 0;
    }
}
