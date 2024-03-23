using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject[] UI_Panels;
    [SerializeField] GameObject loadingPanel;

    [SerializeField] float loadingPanelWaitTime = 1.5f;
    [SerializeField] TextMeshProUGUI loadingText;

    GameManager gameManager;
    AudioManager audioManager;
    void Start()
    {
        Time.timeScale = 1.0f;
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        DeactivateUIPanels();
        loadingPanel.SetActive(false);

    }



    public void PressPlayButton()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {   
        audioManager.audioSource.Stop();
        audioManager.PlayTouchSoundEffect();
        loadingPanel.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        loadingText.text = "Loading.";

        yield return new WaitForSeconds(0.4f);

        loadingText.text = "Loading..";

        yield return new WaitForSeconds(0.5f);
        loadingText.text = "Loading...";

        yield return new WaitForSeconds(loadingPanelWaitTime);
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
