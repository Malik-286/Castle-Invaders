using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnLocker : MonoBehaviour
{
 
    [SerializeField] int defaultInLockLevelNumber = 2;
    [SerializeField] int maxUnlockedLevelNo;

    [SerializeField] Button[] levelsButton;
    [SerializeField] GameObject[] lockedImages;
     



    void Start()
    {
   

        UnlockLevel(defaultInLockLevelNumber);
       

        for (int i = 0; i < levelsButton.Length; i++)
        {
            if (IsLevelUnlocked(i))
            {
                levelsButton[i].interactable = true;
                lockedImages[i].SetActive(false);
             }
            else
            {
                levelsButton[i].interactable = false;
                lockedImages[i].SetActive(true);
            }
        }

        GetTheMaximumUnlockedButton();
    }


    public int GetMaxUnlockedLevelNumber()
    {
        return maxUnlockedLevelNo;
    }

    public void UnlockLevel(int index)
    {
        // Set the level as unlocked in PlayerPrefs
        PlayerPrefs.SetInt("Level" + index, 1);
        PlayerPrefs.Save();

        levelsButton[index].interactable = true;
        lockedImages[index].SetActive(false);
     }
    

    public bool IsLevelUnlocked(int index)
    {
        // Check if the level is unlocked in PlayerPrefs
        return PlayerPrefs.GetInt("Level" + index, 0) == 1;
    }

    public void LoadLevel(int levelToLoad)
    {
       
     
        if (MainMenuUI.instance)
        {
            MainMenuUI.instance.StartCoroutine(MainMenuUI.instance.StartGame());
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
                maxUnlockedLevelNo = maxUnlockedIndex;
            }
        }
    
    }

 
 
}

