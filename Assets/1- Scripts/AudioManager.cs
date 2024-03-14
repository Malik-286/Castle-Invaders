using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [HideInInspector]
   public AudioSource audioSource;
  


    protected override void Awake()
    {
        base.Awake();
     }

      void Start()
    {
        audioSource = GetComponent<AudioSource>();
  
    }

    

    public void PlaySingleShotAudio(AudioClip audioClip, float soundEffect)
    {
        audioSource.PlayOneShot(audioClip,soundEffect);
    }


  

}
