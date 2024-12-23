using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI winRewardText;


    BattleManager battleManager;
    GamePlayUI gamePlayUI;



    void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
        gamePlayUI = FindObjectOfType<GamePlayUI>();
  

        winRewardText.text = battleManager.winAmountToReward.ToString() + " Coins";
        battleManager.RewardPlayerForWin();
        battleManager.DestroyAllTowers();
        gamePlayUI.DisableTowersPanel();

        int nextLevelToUnlock = GameManager.Instance.GetCurrentSceneIndex();
        PlayerPrefs.SetInt("Level" + nextLevelToUnlock, 1);
        PlayerPrefs.Save();
        Debug.Log("New Level: "+ nextLevelToUnlock + " has been unlocked." );
    }

    public void LoadNextLevel()
    {
 

        int index = GameManager.Instance.GetCurrentSceneIndex()+1;
         GameManager.Instance.LoadScene(index);
         AudioManager.Instance.audioSource.Play();
      
         Debug.Log("Loading Next Level: " + index);
    }

    public void GoToMainMenu()
    {
        GameManager.Instance.LoadScene(0);
        AudioManager.Instance.audioSource.Play();       
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
