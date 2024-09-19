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


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #region Give Reward

    public void RewardAfterAd()
    {
        print("Rewarded Ad Index is " + Index);

        if (Index == 0)
        {
            if (RewardsPanel.instance)
            {
                //RewardsPanel.instance.ActivateCoinAnimationPanel();
                StartCoroutine(RewardsPanel.instance.IncreaseGoldCurrency());
            }
        }
        if (Index == 1)
        {
            if (RewardsPanel.instance)
            {
                //RewardsPanel.instance.ActivateJemsAnimationPanel();
                StartCoroutine(RewardsPanel.instance.IncreaseDiamondCurrency());
            }
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