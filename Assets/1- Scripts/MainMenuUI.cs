using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject[] UI_Panels;
  



    GameManager gameManager;
    AudioManager audioManager;
    void Start()
    {
        Time.timeScale = 1.0f;
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        DeactivateUIPanels();
  
    }

    public void PressPlayButton()
    {   
        audioManager.audioSource.Stop();
        audioManager.PlayTouchSoundEffect();
        gameManager.StartGame();
         
    }

    void DeactivateUIPanels()
    {
        foreach (GameObject panels in UI_Panels)
        {
            panels.SetActive(false);
        }
    }
}
