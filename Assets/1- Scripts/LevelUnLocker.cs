using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnLocker : MonoBehaviour
{
    // chnage the level to 2 after making loading slider
    [SerializeField] int defaultInLockLevel = 0;


    [SerializeField] Button[] levelsButton;
    [SerializeField] Image[] lockedImages;


    GameManager gameManager;
 
    void Start()
    {   
        gameManager = FindObjectOfType<GameManager>();



        UnlockLevel(defaultInLockLevel);
       

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
        if(gameManager != null)
        {
            gameManager.LoadScene(levelToLoad);
        }
    }
}

