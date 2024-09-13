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
    public GameObject coinsAnimationPanel;
    public GameObject jemssAnimationPanel;

    CurrencyManager currencyManager;



      void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
        coinsAnimationPanel.SetActive(false);
        jemssAnimationPanel.SetActive(false);
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
            if (currencyManager != null)
            {
                 StartCoroutine(IncreaseGoldCurrency());
                coinsAnimationPanel.GetComponent<Dialog>().ShowDialog();
            }
        }
        if (Index == 2)
        {
            if (currencyManager != null)
            {
                 StartCoroutine(IncreaseDiamondCurrency());
                jemssAnimationPanel.GetComponent<Dialog>().ShowDialog();
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

    IEnumerator IncreaseGoldCurrency()
    {      
        yield return new WaitForSeconds(2.2f);
        currencyManager.IncreaseGold(100);
        currencyManager.SaveCurrencyData();

    }

    IEnumerator IncreaseDiamondCurrency()
    {
        yield return new WaitForSeconds(2.2f);
        currencyManager.IncreaseDiamond(5);
        currencyManager.SaveCurrencyData();

    }
}