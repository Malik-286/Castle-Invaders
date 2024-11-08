using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class Interstitial : MonoBehaviour
{
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-1387627577986386/4701504559";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-1387627577986386/3689673771";
#else
    private string _adUnitId = "unused";
#endif

    InterstitialAd _interstitialAd;
 

    void Start()
    {
         
         MobileAds.Initialize((InitializationStatus initStatus) => { });

        if (PlayerPrefs.GetString("AdsStatusKey") == "disabled")
        {
            return;
        }
        else
        {
            LoadInterstitialAd();
        }
        
    }





    public void LoadInterstitialAd()
    {
        if (PlayerPrefs.GetString("AdsStatusKey") == "disabled")
        {
            return;
        }

        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        var adRequest = new AdRequest();

        InterstitialAd.Load(_adUnitId, adRequest,
           (InterstitialAd ad, LoadAdError error) =>
           {
               if (error != null || ad == null)
               {
                   Debug.LogError("interstitial ad failed to load an ad " +
                                  "with error : " + error);
                   return;
               }

               Debug.Log("Interstitial ad loaded with response : "
                         + ad.GetResponseInfo());

               _interstitialAd = ad;

           });


    }

 
    public void ShowInterstitialAd()
    {
        if (PlayerPrefs.GetString("AdsStatusKey") == "disabled")
        {
            return;
        }
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            if(_interstitialAd.CanShowAd())
            {
                _interstitialAd.Show();
            }
             
        }
        else
        {
            LoadInterstitialAd();

            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
         interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

      void RegisterReloadHandler(InterstitialAd interstitialAd)
    {
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
    {
            Debug.Log("Interstitial Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
    }

}
