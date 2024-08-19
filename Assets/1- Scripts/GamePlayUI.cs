using hardartcore.CasualGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    public static GamePlayUI Instance;

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



    [Header("Death Panel Variables")]
    [SerializeField] GameObject deathPanel;

    [Header("Towers Panel Variables")]
    [SerializeField] GameObject towersPanel;

    [Header("Win Panel Variables")]
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject winParticles;

    [Header("Shop Panel Variables")]
    [SerializeField] GameObject shopPanel;

    [Header("Enemies Pool Variables")]
    [SerializeField] int totalPoolEnemies;

    [Header("Scene Startup Image")]
    [SerializeField] Image fadeImage;

    [Header("Pause Panel")]
    [SerializeField] GameObject pausePanel;

    [Header("Pause Panel")]
    [SerializeField] GameObject giftPanel;

    GameManager gameManager;
    AudioManager audioManager;
    public GameObject Maincamera;
    public GameObject Cutscene;
      void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Time.timeScale = 1.0f;

        StartCoroutine(EndCutscene());

    }
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.GetComponent<AudioSource>().enabled = true;
        gameManager = FindObjectOfType<GameManager>();
        UpdateCurrentLevelText();
        levelStartupTime = levelCompletionTime;
        enemiesKillsCout = 0;
        playerHealth = FindObjectOfType<PlayerCastleHealth>();
        healthSlider.maxValue = playerHealth.GetMaxHealth();
        deathPanel.SetActive(false);
        GetTotalPoolEnemies();
        audioManager.audioSource.Play();
        shopPanel.SetActive(false);
        pausePanel.SetActive(false);
        winParticles.SetActive(false);
        winPanel.SetActive(false);
        giftPanel.SetActive(false);


        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeImageToTransparentWhite(1.5f));

    }

    IEnumerator EndCutscene()
    {
        Cutscene.SetActive(true);
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(12f);
        Cutscene.SetActive(false);
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    void Update()
    {
        UpdateGamePlayTimer();
        UpdateEnemiesCountText();
        UpdatePlayerHealthSlider();
        FixTimeToZero();
    }

    public int GetPoolEnemies()
    {
        return totalPoolEnemies;
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


    void UpdateEnemiesCountText()
    {
        
        enemiesKillsCountText.text = totalPoolEnemies.ToString()+"/"+enemiesKillsCout.ToString();
        
    }

    void UpdatePlayerHealthSlider()
    {
        healthSlider.value = playerHealth.GetCurretHealth();
    }


    public void ActivateDeathPanel()
    {
        audioManager.PlayDeathSoundEffect();
        deathPanel.SetActive(true);
    }

    public void DisableTowersPanel()
    {
        towersPanel.SetActive(false);
    }

    public IEnumerator ActivateWinPanel()
    {
        yield return new WaitForSeconds(3);
        bool isActivated = false;
        if(winPanel != null && isActivated == false)
        {        
            winPanel.SetActive(true);
            isActivated = true;
            winParticles.SetActive(true);        
        }
         
    }




      void GetTotalPoolEnemies()
    {

        GameObject[] enemyPools = GameObject.FindGameObjectsWithTag("EnemiesPool");

 
        foreach (GameObject pool in enemyPools)
        {            
            totalPoolEnemies += pool.GetComponent<ObjectPool>().GetNumberOfPools();
        }
    }

    public void EnableShopPanel()
    {
        shopPanel.SetActive(true);
    }
    public void DisableShopPanel()
    {
        shopPanel.SetActive(false);
    }
    
    IEnumerator FadeImageToTransparentWhite(float fadeDuration)
    {
        Color startColor = Color.black;
        Color targetColor = new Color(0f, 0f, 0f, 0f); // Transparent white

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            fadeImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = targetColor;

         Destroy(fadeImage.gameObject, 2f);
    }

    

    public void PauseGame()
    {
        pausePanel.GetComponent<Dialog>().ShowDialog();
        StartCoroutine(StopGame());  
    }

    void FixTimeToZero()
    {
        if(deathPanel.activeInHierarchy || winPanel.activeInHierarchy)
        {
            timeText.text = ("00:00");
        }
        
    }

    IEnumerator StopGame()
    {
        yield return new WaitForSeconds(0.30f);
        Time.timeScale = 0.0f;
    }
}
