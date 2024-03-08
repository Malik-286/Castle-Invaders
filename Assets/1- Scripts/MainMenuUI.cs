using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject[] ui_Panels;
  



    GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        DeactivateUIPanels();
  
    }

    public void PressPlayButton()
    {         
          gameManager.StartGame();     
    }

    void DeactivateUIPanels()
    {
        foreach (GameObject panels in ui_Panels)
        {
            panels.SetActive(false);
        }
    }
}
