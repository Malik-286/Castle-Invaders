using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{


    GamePlayUI gamePlayUI;
 

    protected override void Awake()
    {
        base.Awake();
                
    }

    void Start()
    {
        gamePlayUI = FindObjectOfType<GamePlayUI>();
     }


    
   


    public void StartGame()
    {    
         SceneManager.LoadScene(2);           
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void RestartCurrentLevel()
    {
        
        SceneManager.LoadScene(GetCurrentSceneIndex());
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }


}

