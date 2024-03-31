using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinPanel : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI winRewardText;


    GameManager gameManager;
    BattleManager battleManager;
    GamePlayUI gamePlayUI;
    AudioManager audioManager;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        battleManager = FindObjectOfType<BattleManager>();
        gamePlayUI = FindObjectOfType<GamePlayUI>();
        audioManager = FindObjectOfType<AudioManager>();


        winRewardText.text = battleManager.winAmountToReward.ToString() + " Coins";
        battleManager.RewardPlayerForWin();
        battleManager.DestroyAllTowers();
        gamePlayUI.DisableTowersPanel();

        int nextLevelToUnlock = gameManager.GetCurrentSceneIndex()-1;
        PlayerPrefs.SetInt("Level" + nextLevelToUnlock, 1);
        PlayerPrefs.Save();
        Debug.Log("New Level: "+ nextLevelToUnlock + " has been unlocked." );
    }

    public void LoadNextLevel()
    {
         int index = gameManager.GetCurrentSceneIndex()+1;
         gameManager.LoadScene(index);
         audioManager.audioSource.Play();
         Debug.Log("Loading Next Level: " + index);
    }

    public void GoToMainMenu()
    {
        gameManager.LoadScene(1);
        audioManager.audioSource.Play();
    }

 

}
