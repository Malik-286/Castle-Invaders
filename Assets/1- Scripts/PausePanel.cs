using hardartcore.CasualGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{


    GameObject towersPanel;
     void Start()
    {

        towersPanel = GameObject.FindGameObjectWithTag("TowersPanel");
     }



    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if(towersPanel != null)
            {
                towersPanel.SetActive(false);
            }
            
        }
    }


    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        if (towersPanel != null)
        {
            towersPanel.SetActive(true);
        }
         gameObject.GetComponent<Dialog>().HideDialog();
         
    }

    public void GoToMianMenu()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.LoadScene(0);
        }     
    }

    public void RestartGame()
    {
        if(GameManager.Instance != null)
        {
            int currentSceneIndex = GameManager.Instance.GetCurrentSceneIndex();
            GameManager.Instance.LoadScene(currentSceneIndex);
          
        }
    }

}
