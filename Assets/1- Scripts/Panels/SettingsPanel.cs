using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    string fbURL = "https://www.facebook.com/profile.php?viewas=100000686899395&id=61556956161259";
    string instaURL = "https://www.instagram.com/aspire_games_studio/?igsh=dGZhemd3bGpiNHh0";
    string youTubeURL = "https://www.youtube.com/@aspiregames286";
    string discoedURL = "https://discord.gg/bMmsU8k8";



    string privayPolicyUrl = "https://sites.google.com/view/castle-invaders-privacy-policy/home?read_current=1";
    string appstoreUrl = "https://apps.apple.com/app/castle-invaders/id6480585969";
    string googlePlayStoreUrl = "https://play.google.com/store/apps/details?id=com.AspireGamesStudio.CastleInvaders&pcampaignid=web_share";

    [SerializeField] Slider volumeSwitch;
    [SerializeField] TextMeshProUGUI gameVersionText;
  

 
     void Start()
    {
         UpdateGameVersionText();
    }


    public void EnableOrDisableVolume()
    {
        if (AudioManager.Instance != null)
        {
            if (volumeSwitch.value == 0)
            {
                AudioManager.Instance.audioSource.Pause();
            }
            else if (volumeSwitch.value == 1)
            {
                AudioManager.Instance.audioSource.Play();
            }

        }
    }

 

    public void OpenFBPage()
    {
        Application.OpenURL(fbURL);
    }

    public void OpenYoutubeChannel()
    {
        Application.OpenURL(youTubeURL);
    }

    public void OpenInstagramPage()
    {
        Application.OpenURL(instaURL);
    }

    public void OpenDircordPage()
    {
        Application.OpenURL(discoedURL);
    }


    public void ViewPrivacyPolicy()
    {
        Application.OpenURL(privayPolicyUrl);
    }


    public void RateUs()
    {
        if (Application.platform != RuntimePlatform.IPhonePlayer)
        {
            // Open Google Play Store URL
            Application.OpenURL(googlePlayStoreUrl);
        }
        else
        {
            // Open Apple App Store URL
            Application.OpenURL(appstoreUrl);
        }
    }

    void UpdateGameVersionText()
    {
        gameVersionText.text = Application.version;
    }

}
