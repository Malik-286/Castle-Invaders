 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : MonoBehaviour
{

    GameManager gameManager;
     void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void LoadNextLevel()
    {
        int nextSceneToLoad = gameManager.GetCurrentSceneIndex() + 1;
        gameManager.LoadScene(nextSceneToLoad);
    }

    public void GoToMainMenu()
    {
        gameManager.LoadScene(1);
    }
}
