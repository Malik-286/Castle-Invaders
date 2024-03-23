using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class ShopPanel : MonoBehaviour
{
   
    
    [SerializeField] GameObject purchaseFailedPanel;
    [SerializeField] GameObject purchasedSucessPanel;
 

     CurrencyManager currencyManager;

      const string starterPack_ProductID = "com.aspiregamesstudio.castleinvaders.starterpack";
      const string removeAds_ProductID = "com.aspiregamesstudio.castleinvaders.removeads";
      const string coinsPack500_ProductID = "com.aspiregamesstudio.castleinvaders.coins500";
      const string coinsPack1000_ProductID = "com.aspiregamesstudio.castleinvaders.coins1000";
      const string coinsPack2500_ProductID = "com.aspiregamesstudio.castleinvaders.coins5000";
      const string coinsPack5000_ProductID = "com.aspiregamesstudio.castleinvaders.coins10000";


    [Header("Ads Status")]
    public string adsStaus = "enabled";


    void Awake()
    {
        purchaseFailedPanel.SetActive(false);
        purchasedSucessPanel.SetActive(false);
        adsStaus = PlayerPrefs.GetString("AdsStatusKey");
        currencyManager = FindObjectOfType<CurrencyManager>();
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == removeAds_ProductID)
        {
            adsStaus = "disabled";
            PlayerPrefs.SetString("AdsStatusKey", adsStaus);
            purchasedSucessPanel.SetActive(true);
        }
        else if(product.definition.id == starterPack_ProductID && currencyManager != null)
        {
            currencyManager.IncreaseGold(500);
            adsStaus = "disabled";
            PlayerPrefs.SetString("AdsStatusKey", adsStaus);
            currencyManager.SaveCurrencyData();
            Debug.Log("Coins 500 has been added to you account with new balance of :"+currencyManager.GetCurrentGold());
            purchasedSucessPanel.SetActive(true);
        }
        else if (product.definition.id == coinsPack500_ProductID && currencyManager != null)
        {
            currencyManager.IncreaseGold(500);
            currencyManager.SaveCurrencyData();
            Debug.Log("Coins 500 has been added to you account with new balance of :" + currencyManager.GetCurrentGold());
            purchasedSucessPanel.SetActive(true);

        }
        else if (product.definition.id == coinsPack1000_ProductID && currencyManager != null)
        {
            currencyManager.IncreaseGold(1000);
            currencyManager.SaveCurrencyData();
            Debug.Log("Coins 1000 has been added to you account with new balance of :" + currencyManager.GetCurrentGold());
            purchasedSucessPanel.SetActive(true);

        }
        else if (product.definition.id == coinsPack2500_ProductID && currencyManager != null)
        {
            currencyManager.IncreaseGold(5000);
            currencyManager.SaveCurrencyData();
            Debug.Log("Coins 5K has been added to you account with new balance of :" + currencyManager.GetCurrentGold());
            purchasedSucessPanel.SetActive(true);

        }
        else if (product.definition.id == coinsPack5000_ProductID && currencyManager != null)
        {
            currencyManager.IncreaseGold(10000);
            currencyManager.SaveCurrencyData();
            Debug.Log("Coins 10K has been added to you account with new balance of :" + currencyManager.GetCurrentGold());
            purchasedSucessPanel.SetActive(true);

        }

    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        purchaseFailedPanel.SetActive(true);
    }

}
