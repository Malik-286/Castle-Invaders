using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathPanel : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI deathRewardText;


     BattleManager battleManager;

    Interstitial interstitial;
 
    void Start()
    {
         
        interstitial = FindObjectOfType<Interstitial>();
        interstitial.LoadInterstitialAd();
        battleManager = FindObjectOfType<BattleManager>();
 
        deathRewardText.text = battleManager.loseAmountToReward.ToString() + " Coins";
        battleManager.RewardPlayerForLose();
        battleManager.DestroyAllTowers();

    }

    public void PlayAgain()
    {
        if (GameManager.Instance)
        {
            if(interstitial != null)
            {
                interstitial.ShowInterstitialAd();
            }
            GameManager.Instance.RestartCurrentLevel();
        }
            
    }

    public void GoToMainMenu()
    {
        if (GameManager.Instance)
        {
            if (interstitial != null)
            {
                interstitial.ShowInterstitialAd();
            }
             GameManager.Instance.LoadScene(0);
        }
    }



}