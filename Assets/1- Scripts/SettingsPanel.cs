using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    string fbURL = "https://www.facebook.com/profile.php?viewas=100000686899395&id=61556956161259";
    string xURL = "https://twitter.com/aspiregames2024";
    string youTubeURL = "https://www.youtube.com/@aspiregames286";
    string discoedURL = "https://discord.gg/bMmsU8k8";



    string privayPolicyUrl = "https://sites.google.com/view/castle-invaders-privacy-policy/home?read_current=1";
    string appstoreUrl = "https://apps.apple.com/app/castle-invaders/id6480585969";
    string googlePlayStoreUrl = "https://play.google.com/store/apps/details?id=com.AspireGamesStudio.CastleInvaders&pcampaignid=web_share";

    [SerializeField] Slider volumeSwitch;
    [SerializeField] Slider musicSwitch;
 

    AudioManager audioManager;
     void Start()
    {
        audioManager  =FindObjectOfType<AudioManager>();
    }


    public void EnableOrDisableVolume()
    {
        if (audioManager != null)
        {
            if (volumeSwitch.value == 0)
            {
                audioManager.audioSource.Pause();
            }
            else if (volumeSwitch.value == 1)
            {
                audioManager.audioSource.Play();
            }

        }
    }

    public void EnableOrDisableMusic()
    {
        if (audioManager != null)
        {
            if (musicSwitch.value == 1)
            {
                audioManager.audioSource.mute = true;
            }
            else if (musicSwitch.value == 0)
            {
                audioManager.audioSource.mute = false;
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

    public void OpenXPage()
    {
        Application.OpenURL(xURL);
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
        // start logicv here 
        Application.OpenURL(googlePlayStoreUrl);
        Application.OpenURL(appstoreUrl);
    }
}
