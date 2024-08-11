using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    

    GamePlayUI gamePlayUI;
    ObjectPool[] objectPools;
    GameManager gameManager;
     PlayerCastleHealth playerCastleHealth;
   
    public int winAmountToReward, loseAmountToReward;
  

    void Start()
    {
        gamePlayUI = FindObjectOfType<GamePlayUI>();
        objectPools = FindObjectsOfType<ObjectPool>();
        gameManager = FindObjectOfType<GameManager>();
        playerCastleHealth = FindObjectOfType<PlayerCastleHealth>();
   
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
        
        CurrencyManager.Instance.IncreaseGold(winAmountToReward);
        CurrencyManager.Instance.SaveCurrencyData();
        Debug.Log("Player has been rewarded with " + winAmountToReward + "as battle winner");   
        return winAmountToReward;
        
    }
    public int RewardPlayerForLose()
    {
        CurrencyManager.Instance.IncreaseGold(loseAmountToReward);
        CurrencyManager.Instance.SaveCurrencyData();
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
         AudioManager.Instance.audioSource.Stop();
         TargetLocater[] towers = FindObjectsOfType<TargetLocater>();

         foreach (TargetLocater tower in towers)
        {
            Destroy(tower.gameObject);
        }
    }


}
