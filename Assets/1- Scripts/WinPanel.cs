using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinPanel : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI winRewardText;


    GameManager gameManager;
    BattleManager battleManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        battleManager = FindObjectOfType<BattleManager>();

        winRewardText.text = battleManager.winAmountToReward.ToString() + " Coins";
        battleManager.RewardPlayerForWin();
        battleManager.DestroyAllTowers();
    }

    public void LoadNextLevel()
    {
         int index = gameManager.GetCurrentSceneIndex()+1;
         gameManager.LoadScene(index);
         Debug.Log("Loading Next Level: " + index);
    }

    public void GoToMainMenu()
    {
        gameManager.LoadScene(1);
    }

}
