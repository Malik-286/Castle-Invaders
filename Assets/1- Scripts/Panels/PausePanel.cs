using hardartcore.CasualGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{

    [SerializeField] Image muteButtonImage;
    [SerializeField] Sprite muteSprite;
    [SerializeField] Sprite UnmuteSprite;


    GameObject towersPanel;
     void Start()
    {

        towersPanel = GameObject.FindGameObjectWithTag("TowersPanel");
    }



    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
     
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


    public void MuteAndUnMuteAudio()
    {
        if(AudioManager.Instance != null)
        {
            if(AudioManager.Instance.audioSource.mute == true)
            {

                AudioManager.Instance.audioSource.mute = false;
                muteButtonImage.sprite = UnmuteSprite;
            }
            else if(AudioManager.Instance.audioSource.mute == false)
            {
                AudioManager.Instance.audioSource.mute = true;
                muteButtonImage.sprite = muteSprite;
            }
        }
    }

}
