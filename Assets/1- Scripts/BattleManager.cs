using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    

    GamePlayUI gamePlayUI;
    ObjectPool[] objectPools;
    GameManager gameManager;
    CurrencyManager currencyManager;
    PlayerCastleHealth playerCastleHealth;
    AudioManager audioManager;
  
    public int winAmountToReward, loseAmountToReward;
  

    void Start()
    {
        gamePlayUI = FindObjectOfType<GamePlayUI>();
        objectPools = FindObjectsOfType<ObjectPool>();
        gameManager = FindObjectOfType<GameManager>();
        playerCastleHealth = FindObjectOfType<PlayerCastleHealth>();
        currencyManager = FindObjectOfType<CurrencyManager>();
        audioManager = FindObjectOfType<AudioManager>();
  
         CalculateRewardAmounts();
 

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
                StartCoroutine(gamePlayUI.ActivateWinPanel());
                                 
            }
        }


    }

    public bool CheckIfAnyEnemyisAlive(ObjectPool pool)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length > 0;
    }


    public int RewardPlayerForWin()
    {
        
        currencyManager.IncreaseGold(winAmountToReward); 
        currencyManager.SaveCurrencyData();
        Debug.Log("Player has been rewarded with " + winAmountToReward + "as battle winner");   
        return winAmountToReward;
        
    }
    public int RewardPlayerForLose()
    {       
        currencyManager.IncreaseGold(loseAmountToReward);
        currencyManager.SaveCurrencyData();
        Debug.Log("Player has been rewarded with " + loseAmountToReward + "as battle loser");
        return loseAmountToReward;

    }


    void CalculateRewardAmounts()
    {
        winAmountToReward = gameManager.GetCurrentSceneIndex() * 50;

        loseAmountToReward = gameManager.GetCurrentSceneIndex() * Random.Range(5, 20);

    }

    public void DestroyAllTowers()
    {
         audioManager.audioSource.Stop();
         TargetLocater[] towers = FindObjectsOfType<TargetLocater>();

         foreach (TargetLocater tower in towers)
        {
            Destroy(tower.gameObject);
        }
    }


}
