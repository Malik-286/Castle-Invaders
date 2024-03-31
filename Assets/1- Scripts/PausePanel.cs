using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{


    GameObject towersPanel;
    GameManager gameManager;
     void Start()
    {
         
        towersPanel = GameObject.FindGameObjectWithTag("TowersPanel");
        gameManager = FindObjectOfType<GameManager>();
    }



    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            towersPanel.SetActive(false);
        }
    }


    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        towersPanel.SetActive(true);
        gameObject.SetActive(false);
         
    }

    public void GoToMianMenu()
    {
        if(gameManager != null)
        {
            gameManager.LoadScene(1);
        }
    }

    public void RestartGame()
    {
        if(gameManager != null)
        {
            // implement reload game logic here
        }
    }


}
