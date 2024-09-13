using GoogleMobileAds.Api;
using System;
using UnityEngine.UI; 
using UnityEngine;
using hardartcore.CasualGUI;
using System.Collections;
public class AdmobRewardedVideo : MonoBehaviour
{
    public static AdmobRewardedVideo Instance;

    public int Index;

    RewardsPanel rewardsPanel;
 


      void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        rewardsPanel = FindObjectOfType<RewardsPanel>();
    }



    #region Give Reward

    public void RewardAfterAd()
    {
        if (Index == 0)
        {
            rewardsPanel.ActivateCoinAnimationPanel();
            StartCoroutine(rewardsPanel.IncreaseGoldCurrency());
        }
        if (Index == 1)
        {
            rewardsPanel.ActivateJemsAnimationPanel();
            StartCoroutine(rewardsPanel.IncreaseDiamondCurrency());
        }    
    }


    #endregion
    public void ShowRewardedVideo()
    {
        if (Adsmanager.Instance)
            Adsmanager.Instance.ShowRewardedVideoAd();
    }
    public void Show_RewardedInterstitial_Video()
    {
        if (Adsmanager.Instance)
            Adsmanager.Instance.ShowRewardedVideoAd();
    } 
 
}