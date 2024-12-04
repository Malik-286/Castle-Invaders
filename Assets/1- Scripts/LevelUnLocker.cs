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


    [SerializeField] Sprite currentLevelGreenSprite;
    [SerializeField] Sprite defaultButtonSprite;



    MainMenuUI mainMenuUI;

    void Start()
    {
        mainMenuUI = FindObjectOfType<MainMenuUI>();
  



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

        GetTheMaximumUnlockedButton();
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
       
     
        if (mainMenuUI != null)
        {
            mainMenuUI.StartCoroutine(mainMenuUI.StartGame());
            StartCoroutine(LoadScene(levelToLoad));
        }
        else
        {
            print("Main Menu Refrence not found");
        }
         
        
    }

    IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(1.3f);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadScene(index);
        }
    }


     
    public void GetTheMaximumUnlockedButton()
    {
        int maxUnlockedIndex = -1;

        // Find the maximum unlocked button index
        for (int i = 0; i < levelsButton.Length; i++)
        {
            if (IsLevelUnlocked(i))
            {
                maxUnlockedIndex = i;
            }
        }

        // Revert all buttons to their default appearance
        for (int i = 0; i < levelsButton.Length; i++)
        {
            if (i == maxUnlockedIndex)
            {
                // Change the maximum unlocked button to the green sprite
                levelsButton[i].GetComponent<Image>().sprite = currentLevelGreenSprite;
            }
            else
            {
                // Revert other buttons to their default appearance
                levelsButton[i].GetComponent<Image>().sprite = defaultButtonSprite; // or set to your default sprite
            }
        }

        // Ensure the next level is not interactable yet
        if (maxUnlockedIndex + 1 < levelsButton.Length)
        {
            levelsButton[maxUnlockedIndex + 1].GetComponent<Image>().sprite = defaultButtonSprite; // Ensure the next button is default
        }
    }

 
 
}

