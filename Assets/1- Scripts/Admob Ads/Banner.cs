using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class Banner : Singleton<Banner>
{
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-1387627577986386/2109649162";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-1387627577986386/5807102043";
#else
    private string _adUnitId = "unused";
#endif


    BannerView bannerView;

 
    protected override void Awake()
    {
        base.Awake();
       
    }
    void Start()
    {

        if (PlayerPrefs.GetString("AdsStatusKey") == "disabled")
        {
            return;
        }
        else
        {
            MobileAds.Initialize((InitializationStatus initStatus) => { });
            LoadAd();
          
        }

         
    }

 

 
  
    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

         if (bannerView != null)
        {
            DestroyBannerView();
            bannerView = null;
        }

        // Create a 320x50 banner at top of the screen
        bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Top);        
    }

 
    public void LoadAd()
    {
        // create an instance of a banner view first.
        if (bannerView == null)
        {
            CreateBannerView();

        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        bannerView.LoadAd(adRequest);
        ListenToAdEvents();
    }

 
    private void ListenToAdEvents()
    {
        // Raised when an ad is loaded into the banner view.
        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }

 
    public void DestroyBannerView()
    {
        if (bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            bannerView.Destroy();
            bannerView = null;
        }
    }




}