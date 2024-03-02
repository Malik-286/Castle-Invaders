using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI currentLevelText;



    [Header("Time Variables")]

    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] float levelCompletionTime;
    [SerializeField] float levelStartupTime;
    public bool isTimeCompleted = false;

    [Header("Enemies Kills Count Variables")]

    [SerializeField] TextMeshProUGUI enemiesKillsCountText;
    public int enemiesKillsCout;

    [Header("Player Health Variables")]
    [SerializeField] Slider healthSlider;
    PlayerCastleHealth playerHealth;


    [Header("Rewards Panel Variables")]
    [SerializeField] GameObject giftPanel;


    [Header("Death Panel Variables")]
    [SerializeField] GameObject deathPanel;

    [Header("Towers Panel Variables")]
    [SerializeField] GameObject towersPanel;

    [Header("Win Panel Variables")]
    [SerializeField] GameObject winPanel;

    GameManager gameManager;
 
    void Start()
    {
        

        gameManager = FindObjectOfType<GameManager>();
        UpdateCurrentLevelText();
        levelStartupTime = levelCompletionTime;
        enemiesKillsCout = 0;
        playerHealth = FindObjectOfType<PlayerCastleHealth>();
        healthSlider.maxValue = playerHealth.GetMaxHealth();
        giftPanel.SetActive(false);
        deathPanel.SetActive(false);

    }

    void Update()
    {
        UpdateGamePlayTimer();
        UpdateEnemiesKills();
        UpdatePlayerHealthSlider();
    }

   
    void UpdateCurrentLevelText()
    {
      currentLevelText.text =  gameManager.GetCurrentSceneName();
    }


    void UpdateGamePlayTimer()
    {
        levelCompletionTime -= Time.smoothDeltaTime;
        timeText.text = levelCompletionTime.ToString("00:00");

        if (levelCompletionTime <= 0.0f)
        {
            isTimeCompleted = true;
            Debug.Log("You Run Out of Time");
            timeText.text = ("00:00");
         
            Time.timeScale = 0.0f;
            // watch video ad to get extra 15 seconds 
            // Implement google admob rewarded ad here 

         }
    }


    public void RestTime()
    {
        levelCompletionTime = 0;
        Time.timeScale = 1.0f;
        levelCompletionTime = levelStartupTime;
    }


    void UpdateEnemiesKills()
    {
        enemiesKillsCountText.text = enemiesKillsCout.ToString();
    }

    void UpdatePlayerHealthSlider()
    {
        healthSlider.value = playerHealth.GetCurretHealth();
    }

    public void ActivateGiftPanel()
    {
        giftPanel.SetActive(true);
    }

    public void ActivateDeathPanel()
    {
        deathPanel.SetActive(true);
    }

    public void DisableTowersPanel()
    {
        towersPanel.SetActive(false);
    }

    public void ActivateWinPanel()
    {
        winPanel.SetActive(true);
    }

}
