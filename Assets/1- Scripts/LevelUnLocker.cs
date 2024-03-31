using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnLocker : MonoBehaviour
{
   
    [SerializeField] int defaultInLockLevelNumber = 2;


    [SerializeField] Button[] levelsButton;
    [SerializeField] Image[] lockedImages;

    GameManager gameManager;
    MainMenuUI mainMenuUI;

    void Start()
    {
        mainMenuUI = FindObjectOfType<MainMenuUI>();
        gameManager = FindObjectOfType<GameManager>();



        UnlockLevel(defaultInLockLevelNumber);
       

        for (int i = 0; i < levelsButton.Length; i++)
        {
            if (IsLevelUnlocked(i))
            {
                levelsButton[i].interactable = true;
                lockedImages[i].enabled = false;
             }
            else
            {
                levelsButton[i].interactable = false;
                lockedImages[i].enabled = true;
            }
        }
    }

    public void UnlockLevel(int index)
    {
        // Set the level as unlocked in PlayerPrefs
        PlayerPrefs.SetInt("Level" + index, 1);
        PlayerPrefs.Save();

        levelsButton[index].interactable = true;
        lockedImages[index].enabled = false;
     }

    public bool IsLevelUnlocked(int index)
    {
        // Check if the level is unlocked in PlayerPrefs
        return PlayerPrefs.GetInt("Level" + index, 0) == 1;
    }

    public void LoadLevel(int levelToLoad)
    {
        
        mainMenuUI.StartCoroutine(mainMenuUI.StartGame());
        StartCoroutine(LoadScene(levelToLoad));
        
    }

    IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(1.3f);
        if (gameManager != null)
        {
            gameManager.LoadScene(index);
        }
    }

 
 
}

