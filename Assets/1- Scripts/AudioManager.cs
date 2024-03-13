using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [HideInInspector]
    AudioSource audioSource;
    GameManager gameManager;

    [SerializeField] AudioClip mainMenuSFX;
    [SerializeField] AudioClip GamePlaySFX;


    protected override void Awake()
    {
        base.Awake();
     }

      void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();

    }

    private void Update()
    {
        UpdateGamePlayAudio();  
    }

    public void PlaySingleShotAudio(AudioClip audioClip, float soundEffect)
    {
        audioSource.PlayOneShot(audioClip,soundEffect);
    }


    void  UpdateGamePlayAudio()
    {
        if(gameManager && audioSource != null)
        {
            if(gameManager.GetCurrentSceneName() == "Main Menu")
            {
                audioSource.clip = mainMenuSFX;
            }else
            {
                audioSource.clip = GamePlaySFX;
            }
        }
    }
  
}
