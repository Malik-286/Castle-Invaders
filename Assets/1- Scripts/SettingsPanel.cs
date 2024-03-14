using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{

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



}
