using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanel : MonoBehaviour
{

    GameManager gameManager;
     void Start()
    {
     gameManager = FindObjectOfType<GameManager>();   
    }

    public void PlayAgain()
    {
        gameManager.RestartCurrentLevel();
    }

    public void GoToMainMenu()
    {
        gameManager.LoadScene(0);
    }
 
}
