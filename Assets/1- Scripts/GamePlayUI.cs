using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
     GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

     void Update()
    {
        
    }

    public void GoToMainMenu()
    {
        gameManager.LoadScene(0);
    }
}
