using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{

    public static MainMenuUI instance;

    public GameObject[] UI_Panels;
    [SerializeField] GameObject loadingPanel;

    [SerializeField] float loadingPanelWaitTime = 1.5f;
    [SerializeField] TextMeshProUGUI loadingText;

    [SerializeField] GameObject levelsPanel;

    public GameObject RewardPanel;
    public TextMeshProUGUI RewardPanelText;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }    
    }
    void Start()
    {
        Time.timeScale = 1.0f;
 
        DeactivateUIPanels();
        loadingPanel.SetActive(false);
        levelsPanel.SetActive(false);
    }



    public void PressPlayButton()
    {
          levelsPanel.SetActive(true);

    }

   public IEnumerator StartGame()
    {
        if (AudioManager.Instance.audioSource)
        {
            AudioManager.Instance.audioSource.Stop();
            AudioManager.Instance.PlayTouchSoundEffect();
        }
        loadingPanel.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        loadingText.text = "Loading.";

        yield return new WaitForSeconds(0.4f);

        loadingText.text = "Loading..";

        yield return new WaitForSeconds(0.5f);
        loadingText.text = "Loading...";

        yield return new WaitForSeconds(loadingPanelWaitTime);
       
         
    }

    public void ClaimReward(int RewardIndex) //0 for Diamond, 1 for Gold
    {
        if (RewardIndex == 0)
        {
            RewardPanel.transform.GetChild(0).gameObject.SetActive(true);
            RewardPanel.transform.GetChild(2).gameObject.SetActive(false);
            Invoke(nameof(DisableRewardPanel), 1f);
        }
        if (RewardIndex == 1)
        {
            RewardPanel.transform.GetChild(1).gameObject.SetActive(true);
            RewardPanel.transform.GetChild(2).gameObject.SetActive(false);
            Invoke(nameof(DisableRewardPanelCoins), 2f);
        }
            RewardPanel.transform.GetChild(3).gameObject.SetActive(false);
            RewardPanel.transform.GetChild(4).gameObject.SetActive(false);
    }

    public void DisableRewardPanel()
    {
        RewardPanel.transform.GetChild(0).gameObject.SetActive(false);
        RewardPanel.transform.GetChild(2).gameObject.SetActive(true);
        RewardPanel.SetActive(false);
        if (CurrencyManager.Instance)
        {
            CurrencyManager.Instance.IncreaseDiamond(5);
            CurrencyManager.Instance.SaveCurrencyData();

        }
    }

    public void DisableRewardPanelCoins()
    {
        RewardPanel.transform.GetChild(1).gameObject.SetActive(false);
        RewardPanel.transform.GetChild(2).gameObject.SetActive(true);
        RewardPanel.SetActive(false);
        if (CurrencyManager.Instance)
        {
            CurrencyManager.Instance.IncreaseGold(100);
            CurrencyManager.Instance.SaveCurrencyData();

        }
    }

    void DeactivateUIPanels()
    {
        foreach (GameObject panels in UI_Panels)
        {
            panels.SetActive(false);
        }
    }

     
}
