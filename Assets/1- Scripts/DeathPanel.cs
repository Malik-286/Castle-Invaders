using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathPanel : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI deathRewardText;


     BattleManager battleManager;
 
    void Start()
    {         
        battleManager = FindObjectOfType<BattleManager>();
 
        deathRewardText.text = battleManager.loseAmountToReward.ToString() + " Coins";
        battleManager.RewardPlayerForLose();
        battleManager.DestroyAllTowers();

    }

    public void PlayAgain()
    {
     
        if (GameManager.Instance)
        {
            GameManager.Instance.RestartCurrentLevel();
        }
            
    }

    public void GoToMainMenu()
    {
     
        if (GameManager.Instance)
        {
            GameManager.Instance.LoadScene(0);
        }
    }



}