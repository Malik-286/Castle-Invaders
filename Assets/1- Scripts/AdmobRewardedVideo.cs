
  
//using GoogleMobileAds.Api;
using System;
using UnityEngine.UI; 
using UnityEngine;
//using GoogleMobileAds.Sample;
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
                MainMenuUI.instance.RewardPanel.transform.GetChild(5).gameObject.SetActive(true);
                MainMenuUI.instance.RewardPanel.transform.GetChild(4).gameObject.SetActive(false);
                MainMenuUI.instance.RewardPanelText.text = "YOU GOT 100 Coins".ToString();
                MainMenuUI.instance.RewardPanel.SetActive(true);
                MainMenuUI.instance.UI_Panels[1].SetActive(false);
            }
        }
        if (Index == 1)
        {
            if (MainMenuUI.instance)
            {
                MainMenuUI.instance.RewardPanel.transform.GetChild(5).gameObject.SetActive(false);
                MainMenuUI.instance.RewardPanel.transform.GetChild(4).gameObject.SetActive(true);
                MainMenuUI.instance.RewardPanelText.text = "YOU GOT 5 GEMS".ToString();
                MainMenuUI.instance.RewardPanel.SetActive(true);
                MainMenuUI.instance.UI_Panels[1].SetActive(false);
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
        if (UnityAdsManager.instance)
            UnityAdsManager.instance.ShowRewardedVideoAd();
    }
    public void Show_RewardedInterstitial_Video()
    {
        if (UnityAdsManager.instance)
            UnityAdsManager.instance.ShowRewardedVideoAd();
    }
}
