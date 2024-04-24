using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : Singleton<InterstitialAd>, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;

    protected override void Awake()
    {
        base.Awake();

        if (PlayerPrefs.GetString("AdsStatusKey") == "disabled")
        {
            return;
        }
        else
        {
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                 ? _iOsAdUnitId
                 : _androidAdUnitId;

            InvokeRepeating("LoadAd", 45f, 45f);

        }
    }

     public void LoadAd()
    {
         Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
        ShowAd();
    }

     public void ShowAd()
    {
         Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

     public void OnUnityAdsAdLoaded(string adUnitId)
    {
     }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
     }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
     }

    public void OnUnityAdsShowStart(string _adUnitId) { }
    public void OnUnityAdsShowClick(string _adUnitId) { }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) { }
}