using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{


    PlayerCastleHealth playerCastleHealth;
    GamePlayUI gamePlayUI;
    ObjectPool objectPool;


    protected override void Awake()
    {
        base.Awake();
                
    }

    void Start()
    {
        playerCastleHealth = FindObjectOfType<PlayerCastleHealth>();
        gamePlayUI = FindObjectOfType<GamePlayUI>();
        objectPool = FindObjectOfType<ObjectPool>();
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

