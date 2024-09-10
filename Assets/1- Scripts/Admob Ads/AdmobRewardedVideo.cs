using GoogleMobileAds.Api;
using System;
using UnityEngine.UI; 
using UnityEngine;
using GoogleMobileAds.Sample;
using UnityEngine.SceneManagement;
using hardartcore.CasualGUI;
public class AdmobRewardedVideo : MonoBehaviour
{
    public static AdmobRewardedVideo Instance;

    public int Index;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #region Give Reward

    public void RewardAfterAd()
    {
        if (Index == 0)
        {
            if (MainMenuUI.instance)
            {
                MainMenuUI.instance.UI_Panels[1].GetComponent<Dialog>().ShowDialog();
            }
        }
        if (Index == 1)
        {
            if (CurrencyManager.Instance)
            {
                CurrencyManager.Instance.IncreaseGold(50);
                CurrencyManager.Instance.SaveCurrencyData();
            }
        }
        if (Index == 2)
        {
            if (CurrencyManager.Instance)
            {
                CurrencyManager.Instance.IncreaseGold(200);
                CurrencyManager.Instance.SaveCurrencyData();

            }
        }
        if (Index == 3)
        {
            if (CurrencyManager.Instance)
            {
                CurrencyManager.Instance.IncreaseDiamond(5);
                CurrencyManager.Instance.SaveCurrencyData();

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