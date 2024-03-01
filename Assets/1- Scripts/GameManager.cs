using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    
    protected override void Awake()
    {
        base.Awake();
                
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
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }
}

