using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [HideInInspector]
    AudioSource audioSource;
    GameManager gameManager;

    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip gameplayMusic;


    protected override void Awake()
    {
        base.Awake();
     }

      void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        StartGameplayMusic();

    }

      void Update()
    {
        
    }

    public void PlaySingleShotAudio(AudioClip audioClip, float soundEffect)
    {
        audioSource.PlayOneShot(audioClip,soundEffect);
    }


    public void StartMainMenuMusic()
    {
        audioSource.clip = mainMenuMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StartGameplayMusic()
    {
        audioSource.clip = gameplayMusic;
        audioSource.loop = true;
        audioSource.Play();
    }



}
