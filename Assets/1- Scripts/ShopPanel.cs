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

 
      const string removeAds_ProductID = "com.agsventures.castleinvaderswarzone.removeads";

      const string jemsPack100_ProductID = "com.agsventures.castleinvaderswarzone.jems100";
      const string jemsPack500_ProductID = "com.agsventures.castleinvaderswarzone.jems500";

   
      const string coinsPack1000_ProductID = "com.agsventures.castleinvaderswarzone.coins1000";
      const string coinsPack5000_ProductID = "com.agsventures.castleinvaderswarzone.coins5000";  
      const string coinsPack10000_ProductID = "com.agsventures.castleinvaderswarzone.coins10000";


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
       
        purchaseFailedPanel.SetActive(false);
        purchasedSucessPanel.SetActive(false);
          
    }


    void CheckRestoreButton()
    {
        if(Application.platform != RuntimePlatform.IPhonePlayer)
        {
            restoreButton.SetActive(false);
        }
    }

   

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == removeAds_ProductID)
        {
            adsStatus = "disabled";
            PlayerPrefs.SetString("AdsStatusKey", adsStatus);
            purchasedSucessPanel.GetComponent<Dialog>().ShowDialog();
        }
       
        else if (product.definition.id == jemsPack100_ProductID && CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.IncreaseDiamond(100);
            CurrencyManager.Instance.SaveCurrencyData();
            Debug.Log("100 Diamonds has been added to you account with new balance of :" + CurrencyManager.Instance.GetCurrentDiamond());
            purchasedSucessPanel.GetComponent<Dialog>().ShowDialog();

        }
        else if (product.definition.id == jemsPack500_ProductID && CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.IncreaseDiamond(500);
            CurrencyManager.Instance.SaveCurrencyData();
            Debug.Log("500 Diamonds has been added to you account with new balance of :" + CurrencyManager.Instance.GetCurrentDiamond());
            purchasedSucessPanel.GetComponent<Dialog>().ShowDialog();

        }
        else if (product.definition.id == coinsPack1000_ProductID && CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.IncreaseGold(1000);
            CurrencyManager.Instance.SaveCurrencyData();
            Debug.Log("Coins 1000 has been added to you account with new balance of :" + CurrencyManager.Instance.GetCurrentGold());
            purchasedSucessPanel.GetComponent<Dialog>().ShowDialog();

        }
        else if (product.definition.id == coinsPack5000_ProductID && CurrencyManager.Instance != null)
        {

            CurrencyManager.Instance.IncreaseGold(5000);
            CurrencyManager.Instance.SaveCurrencyData();
            Debug.Log("Coins 5K has been added to your account with new balance of :" + CurrencyManager.Instance.GetCurrentGold());
            purchasedSucessPanel.GetComponent<Dialog>().ShowDialog();

        }
        else if (product.definition.id == coinsPack10000_ProductID && CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.IncreaseGold(10000);
            CurrencyManager.Instance.SaveCurrencyData();
            Debug.Log("Coins 10K has been added to you account with new balance of :" + CurrencyManager.Instance.GetCurrentGold());
            purchasedSucessPanel.GetComponent<Dialog>().ShowDialog();

        }

    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        purchaseFailedPanel.GetComponent<Dialog>().ShowDialog();
    }


    
}
