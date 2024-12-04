
  
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI; 
using UnityEngine;
using GoogleMobileAds.Sample;
using UnityEngine.SceneManagement;
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
                MainMenuUI.instance.RewardPanel.transform.GetChild(4).gameObject.SetActive(true);
                MainMenuUI.instance.RewardPanel.transform.GetChild(3).gameObject.SetActive(false);
                MainMenuUI.instance.RewardPanelText.text = "GREAT WORK!\r\nYOU GOT 100 Coins".ToString();
                MainMenuUI.instance.RewardPanel.SetActive(true);
            }
        }
        if (Index == 1)
        {
            if (MainMenuUI.instance)
            {
                MainMenuUI.instance.RewardPanel.transform.GetChild(4).gameObject.SetActive(false);
                MainMenuUI.instance.RewardPanel.transform.GetChild(3).gameObject.SetActive(true);
                MainMenuUI.instance.RewardPanelText.text = "GREAT WORK!\r\nYOU GOT 5 GEMS".ToString();
                MainMenuUI.instance.RewardPanel.SetActive(true);
            }
        }
        if (Index == 2)
        {
            if (GamePlayUI.Instance)
            {
                GamePlayUI.Instance.RewardPanel.SetActive(true);
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
