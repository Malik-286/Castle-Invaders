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
    [SerializeField] GameObject SkipButton;

    [Header("Pause Panel")]
    [SerializeField] GameObject pausePanel;

    [Header("Reward Panel")]
    public GameObject RewardPanel;

    [Header("Defence Power Slider")]
    [SerializeField] Slider defencePowerSlider;

    [Header("Attack Power Slider")]
    [SerializeField] Slider attackPowerSlider;

    GameManager gameManager;
    AudioManager audioManager;
    [HideInInspector] public GameObject Maincamera;
    public GameObject Cutscene;
    public bool StartingLevels;
    public bool MiddleLevels;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Time.timeScale = 1.0f;

        StartCoroutine(EndCutscene());

        levelCompletionTime = GetCurrentLevelCompletionTime();

        SkipButton.SetActive(false);
        Invoke(nameof(ActivateSkipCutsceneButton), 2f);

    }
    void Start()
    {
        audioManager = AudioManager.Instance;
        audioManager.GetComponent<AudioSource>().enabled = true;
        gameManager = GameManager.Instance;
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
        Maincamera = Camera.main.gameObject;
 

        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeImageToTransparentWhite(1.5f));

    }

    public void ActivateSkipCutsceneButton()
    {
        SkipButton.SetActive(true);
    }
    public void SkipTheCutsceneNow()
    {
        Cutscene.SetActive(false);
        SkipButton.SetActive(false);
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    IEnumerator EndCutscene()
    {
        Cutscene.SetActive(true);
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        if (StartingLevels)
        {
            yield return new WaitForSeconds(12f);
        }
        else
        {
            if (MiddleLevels)
            {
                yield return new WaitForSeconds(20f);
            }
            else
            {
            yield return new WaitForSeconds(18f);
            }
        }
        Cutscene.SetActive(false);
        SkipButton.SetActive(false);
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    void Update()
    {
        UpdateGamePlayTimer();
        UpdateEnemiesCountText();
        UpdatePlayerHealthSlider();
        FixTimeToZero();
        UpdateDefencePowerValue();
        UpdateAttackPowerValue();
    }

    public int GetPoolEnemies()
    {
        return totalPoolEnemies;
    }

   
    void UpdateCurrentLevelText()
    {
        if(GameManager.Instance)
        {
            currentLevelText.text = GameManager.Instance.GetCurrentSceneName();

        }
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

    public void ClaimReward()
    {
        if (CurrencyManager.Instance)
        {
            CurrencyManager.Instance.IncreaseDiamond(5);
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


    public void ClaimGameplayGEMS()
    {
        RewardPanel.transform.GetChild(0).gameObject.SetActive(true);
        RewardPanel.transform.GetChild(1).gameObject.SetActive(false);

        Invoke(nameof(DisableRewardPanel), 1f);
    }

    public void DisableRewardPanel()
    {
        RewardPanel.transform.GetChild(0).gameObject.SetActive(false);
        RewardPanel.transform.GetChild(1).gameObject.SetActive(true);
        RewardPanel.SetActive(false);
        if (CurrencyManager.Instance)
        {
            CurrencyManager.Instance.IncreaseDiamond(5);
            CurrencyManager.Instance.SaveCurrencyData();

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


    void UpdateDefencePowerValue()
    {
        if(BattleManager.instance.missionCompleted)
        {
            return;
        }
        defencePowerSlider.maxValue = 5;

        defencePowerSlider.value = DefencePowerManager.instance.GetTotalTowersInSceneValue();
    }

    void UpdateAttackPowerValue()
    {
        if (BattleManager.instance.missionCompleted)
        {
            return;
        }
        attackPowerSlider.maxValue = totalPoolEnemies;

        attackPowerSlider.value = AttackPowerManager.instance.GetTotalEnemiesInSceneValue();
    }


    public float GetCurrentLevelCompletionTime()
    {
        if (GameManager.Instance)
        {
            int levelIndex = GameManager.Instance.GetCurrentSceneIndex();
 
            // Ensure levelIndex is within the valid range (1 to 100)
            if (levelIndex >= 1 && levelIndex <= 100)
            {
                // Calculate the time based on the level index.
                // Divide the levelIndex by 5 (integer division) to determine the "block" of 5 levels.
                int timeBlock = (levelIndex - 1) / 5;  // Subtract 1 to ensure level 1 starts with time 140f

                // The starting time is 140f for levels 1-5, then it increases by 5 units every 5 levels.
                float timeForLevel = 145f + (timeBlock * 5);

                 return timeForLevel;
            }
            else
            {
                Debug.LogWarning("Level index out of range: " + levelIndex); // Level index is out of the expected range.
            }
        }
        else
        {
            Debug.LogError("GameManager.Instance is null!"); // If GameManager is null, log an error.
        }

         return 0f;
    }

   



}
