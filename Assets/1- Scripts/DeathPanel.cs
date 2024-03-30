using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathPanel : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI deathRewardText;

 
    GameManager gameManager;
    BattleManager battleManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        battleManager = FindObjectOfType<BattleManager>();

        deathRewardText.text = battleManager.loseAmountToReward.ToString() + " Coins";
        battleManager.RewardPlayerForLose();
        battleManager.DestroyAllTowers();

    }

    public void PlayAgain()
    {
        gameManager.RestartCurrentLevel();
    }

    public void GoToMainMenu()
    {
        gameManager.LoadScene(0);
    }

    


}