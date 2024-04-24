using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{
    

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.TOP_CENTER;

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null;  



    void Awake()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
        if (PlayerPrefs.GetString("AdsStatusKey") == "disabled")
        {
            return;
        }
        else
        {
            Advertisement.Banner.SetPosition(_bannerPosition);
            LoadBanner();
            ShowBannerAd();
        }
    }
    void Start()
    {

        if (PlayerPrefs.GetString("AdsStatusKey") == "disabled")
        {
            return;
        }
        else
        {
            Advertisement.Banner.SetPosition(_bannerPosition);
            LoadBanner();
            ShowBannerAd();
        }
    }

     public void LoadBanner()
    {
         BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

         Advertisement.Banner.Load(_adUnitId, options);
        ShowBannerAd() ;
    }

     void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");

     
    }

     void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
     }

     void ShowBannerAd()
    {
         BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

         Advertisement.Banner.Show(_adUnitId, options);
    }

     void HideBannerAd()
    {
         Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

 
}