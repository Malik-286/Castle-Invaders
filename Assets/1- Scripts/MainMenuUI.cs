using hardartcore.CasualGUI;
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
    [SerializeField] GameObject quitGamePanel;



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
        quitGamePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {      
            ActivateQuitGamePanel();
        }
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

  

   

    void DeactivateUIPanels()
    {
        foreach (GameObject panels in UI_Panels)
        {
            panels.SetActive(false);
        }
    }

    public void ActivateQuitGamePanel()
    {
        if (quitGamePanel != null)
        {
            if (quitGamePanel.gameObject.activeInHierarchy)
            {
                quitGamePanel.gameObject.GetComponent<Dialog>().HideDialog();
                return;
            }

            quitGamePanel.gameObject.GetComponent<Dialog>().ShowDialog();

        }
    }

     
}
