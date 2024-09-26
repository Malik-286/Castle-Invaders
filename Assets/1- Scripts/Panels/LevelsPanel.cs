using hardartcore.CasualGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsPanel : MonoBehaviour
{

    [SerializeField] AudioClip whooshSFX;
    [SerializeField] GameObject[] levelsPanels;

    [SerializeField] Image[] panels_Counts;

    int currentPanelIndex = 0;
    private const string PanelIndexKey = "CurrentPanelIndex";

 
    void Start()
    {
 
        // Load the saved panel index or default to 0 if not set
        currentPanelIndex = PlayerPrefs.GetInt(PanelIndexKey, 0);
        ShowCurrentPanel();
    }

    public void LeftClick()
    {
        
        PlayTapSoundEffect();   
        // Move to the previous panel with circular wrap-around
        currentPanelIndex--;
        if (currentPanelIndex < 0)
        {
            currentPanelIndex = levelsPanels.Length - 1;
        }
        SavePanelIndex();
        ShowCurrentPanel();
    }

    public void RightClick()
    {
        PlayTapSoundEffect();

        // Move to the next panel with circular wrap-around
        currentPanelIndex++;
        if (currentPanelIndex >= levelsPanels.Length)
        {
            currentPanelIndex = 0;
        }
        SavePanelIndex();
        ShowCurrentPanel();
    }

    void ShowCurrentPanel()
    {
        // Hide all panels
        foreach (GameObject panel in levelsPanels)
        {
            panel.SetActive(false);
        }

        // Show the current panel
        levelsPanels[currentPanelIndex].SetActive(true);

        // Adjust panel count sizes
        AdjustPanelCountSizes();

        // Optionally, show a dialog for the current panel
        levelsPanels[currentPanelIndex].GetComponent<Dialog>().ShowDialog();
    }

    private void AdjustPanelCountSizes()
    {
        // Loop through all panel counts and adjust their sizes
        for (int i = 0; i < panels_Counts.Length; i++)
        {
            if (i == currentPanelIndex)
            {
                // Set the active panel's size to 1.1
                panels_Counts[i].transform.localScale = new Vector2(1.1f, 1.1f);
            }
            else
            {
                // Minimize other panels' sizes to 0.5
                panels_Counts[i].transform.localScale = new Vector2(0.5f, 0.5f);
            }
        }
    }

    private void SavePanelIndex()
    {
        // Save the current panel index to PlayerPrefs
        PlayerPrefs.SetInt(PanelIndexKey, currentPanelIndex);
        PlayerPrefs.Save(); // Ensure the data is saved immediately
    }

    public void PlayTapSoundEffect()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySingleShotAudio(whooshSFX, 1.0f);
        }
    }
}
