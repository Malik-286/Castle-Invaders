using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinPanel : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI winRewardText;


    BattleManager battleManager;
    GamePlayUI gamePlayUI;

    Interstitial interstitial;


    void Start()
    {

        interstitial = FindObjectOfType<Interstitial>();
        if(interstitial != null)
        {
            interstitial.LoadInterstitialAd();
        }
         


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

        if (interstitial != null)
        {
            interstitial.ShowInterstitialAd();
        }


        int index = GameManager.Instance.GetCurrentSceneIndex()+1;
         GameManager.Instance.LoadScene(index);
         AudioManager.Instance.audioSource.Play();
      
         Debug.Log("Loading Next Level: " + index);
    }

    public void GoToMainMenu()
    {

        if (interstitial != null)
        {
            interstitial.ShowInterstitialAd();
        }

        GameManager.Instance.LoadScene(0);
        AudioManager.Instance.audioSource.Play();
        
    }

 

}
