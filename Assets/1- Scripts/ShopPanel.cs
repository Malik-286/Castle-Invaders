using hardartcore.CasualGUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class ShopPanel : MonoBehaviour
{

    [SerializeField] GameObject restoreButton;
   
    
    [SerializeField] GameObject purchaseFailedPanel;
    [SerializeField] GameObject purchasedSucessPanel;

    [SerializeField] TextMeshProUGUI starterPackTimeText;
    [SerializeField] float toltalSeconds;

    [SerializeField] GameObject starterPackButton;
    const string starterPackPurchasedKey = "StarterPackPurchased";  




    CurrencyManager currencyManager;

      const string starterPack_ProductID = "com.aspiregamesstudio.castleinvaders.starterpack";
      const string removeAds_ProductID = "com.aspiregamesstudio.castleinvaders.removeads";
      const string jemsPack100_ProductID = "com.aspiregamesstudio.castleinvaders.jems100";
      const string jemsPack500_ProductID = "com.aspiregamesstudio.castleinvaders.jems500";

   
      const string coinsPack1000_ProductID = "com.aspiregamesstudio.castleinvaders.coins1000";
      const string coinsPack5000_ProductID = "com.aspiregamesstudio.castleinvaders.coins5000";  
      const string coinsPack10000_ProductID = "com.aspiregamesstudio.castleinvaders.coins10000";


    [Header("Ads Status")]

    public string adsStatus;
    public string defaultAdsStatus = "enabled";


    void Awake()
    {

        CheckRestoreButton();

        adsStatus = PlayerPrefs.GetString("AdsStatusKey");
        if(adsStatus == string.Empty)
        {
            this.adsStatus = "enabled";
        }
        // Check if the starter pack has been purchased
        if (PlayerPrefs.GetInt(starterPackPurchasedKey, 0) == 1)
        {
            // If purchased, disable the starter pack button
            starterPackButton.SetActive(false);
        }
        purchaseFailedPanel.SetActive(false);
        purchasedSucessPanel.SetActive(false);
        currencyManager = FindObjectOfType<CurrencyManager>();
         
    }


    void CheckRestoreButton()
    {
        if(Application.platform != RuntimePlatform.IPhonePlayer)
        {
            restoreButton.SetActive(false);
        }
    }

     void Update()
    {
        StarterPackDealTimer();
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == removeAds_ProductID)
        {
            adsStatus = "disabled";
            PlayerPrefs.SetString("AdsStatusKey", adsStatus);
            purchasedSucessPanel.GetComponent<Dialog>().ShowDialog();
        }
        else if(product.definition.id == starterPack_ProductID && currencyManager != null)
        {
            currencyManager.IncreaseGold(500);
            adsStatus = "disabled";
            PlayerPrefs.SetString("AdsStatusKey", adsStatus);
            currencyManager.SaveCurrencyData();
            purchasedSucessPanel.GetComponent<Dialog>().ShowDialog();

            // Mark the starter pack as purchased
            PlayerPrefs.SetInt(starterPackPurchasedKey, 1);

            // Disable the starter pack button
            starterPackButton.SetActive(false);
        }
        else if (product.definition.id == jemsPack100_ProductID && currencyManager != null)
        {
            currencyManager.IncreaseDiamond(100);
            currencyManager.SaveCurrencyData();
            Debug.Log("100 Diamonds has been added to you account with new balance of :" + currencyManager.GetCurrentDiamond());
            purchasedSucessPanel.GetComponent<Dialog>().ShowDialog();

        }
        else if (product.definition.id == jemsPack500_ProductID && currencyManager != null)
        {
            currencyManager.IncreaseDiamond(500);
            currencyManager.SaveCurrencyData();
            Debug.Log("500 Diamonds has been added to you account with new balance of :" + currencyManager.GetCurrentDiamond());
            purchasedSucessPanel.GetComponent<Dialog>().ShowDialog();

        }
        else if (product.definition.id == coinsPack1000_ProductID && currencyManager != null)
        {
            currencyManager.IncreaseGold(1000);
            currencyManager.SaveCurrencyData();
            Debug.Log("Coins 1000 has been added to you account with new balance of :" + currencyManager.GetCurrentGold());
            purchasedSucessPanel.GetComponent<Dialog>().ShowDialog();

        }
        else if (product.definition.id == coinsPack5000_ProductID && currencyManager != null)
        {
                                           
            currencyManager.IncreaseGold(5000);
            currencyManager.SaveCurrencyData();
            Debug.Log("Coins 5K has been added to your account with new balance of :" + currencyManager.GetCurrentGold());
            purchasedSucessPanel.GetComponent<Dialog>().ShowDialog();

        }
        else if (product.definition.id == coinsPack10000_ProductID && currencyManager != null)
        {
            currencyManager.IncreaseGold(10000);
            currencyManager.SaveCurrencyData();
            Debug.Log("Coins 10K has been added to you account with new balance of :" + currencyManager.GetCurrentGold());
            purchasedSucessPanel.GetComponent<Dialog>().ShowDialog();

        }

    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        purchaseFailedPanel.GetComponent<Dialog>().ShowDialog();
    }


    void StarterPackDealTimer()
    {
        // Load the saved time from PlayerPrefs
        float savedTime = PlayerPrefs.GetFloat("StarterPackDealTime", toltalSeconds);

        // Calculate the remaining time
        float remainingTime = savedTime - Time.deltaTime;

        // Check if the remaining time is less than or equal to 0
        if (remainingTime <= 0)
        {
            // Reset remaining time to default (50 minutes)
            remainingTime = toltalSeconds;
      
        }

        // Save the remaining time back to PlayerPrefs
        PlayerPrefs.SetFloat("StarterPackDealTime", remainingTime);

        // Convert remaining time to hours, minutes, and seconds
        int hours = (int)(remainingTime / 3600);
        int minutes = (int)((remainingTime % 3600) / 60);
        int seconds = (int)(remainingTime % 60);

        // Update the text to display remaining time
        starterPackTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
