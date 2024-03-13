using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{


    GamePlayUI gamePlayUI;
    ObjectPool objectPool;
    LevelUnLocker levelUnlocker;


    protected override void Awake()
    {
        base.Awake();
                
    }

    void Start()
    {
        gamePlayUI = FindObjectOfType<GamePlayUI>();
        objectPool = FindObjectOfType<ObjectPool>();
        levelUnlocker = FindObjectOfType<LevelUnLocker>();
    }


      void Update()
    {
        CheckPlayerWin();
    }


   


    public void StartGame()
    {    
            SceneManager.LoadScene(1);           
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


    void CheckPlayerWin()
    {
        if (objectPool != null)
        {
            if (objectPool.GetNumberOfPools() <= 0 && !CheckIfAnyEnemyisAlive())
            {
                Debug.Log("Player has won the game");
                StartCoroutine(gamePlayUI.ActivateWinPanel());
                levelUnlocker.UnlockLevel(GetCurrentSceneIndex());

                return;
            }
            else
            {
                Debug.Log("War is happening...");
                return;
            }
        }
         
    }

    bool CheckIfAnyEnemyisAlive()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length > 0;
    }

}

