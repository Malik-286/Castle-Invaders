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
}

