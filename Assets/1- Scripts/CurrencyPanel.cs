using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyPanel : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI goldcurrencyText;
    [SerializeField] TextMeshProUGUI diamondcurrencyText;


    CurrencyManager currencyManager;
     void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
    }

    void Update()
    {
        UpdateGoldCurrencyText();
        UpdateDiamondCurrencyText();
    }

   

    void UpdateGoldCurrencyText()
    {
        int currentGold = currencyManager.GetCurrentGold();
        if (currentGold >= 1000)
        {
            float goldInK = currentGold / 1000f;
            goldcurrencyText.text = goldInK.ToString("0.#") + "k";
        }
        else
        {
            goldcurrencyText.text = currentGold.ToString();
        }
    }

    void UpdateDiamondCurrencyText()
    {
           int currentDiamond = currencyManager.GetCurrentDiamond();
       
             diamondcurrencyText.text = currentDiamond.ToString();         
    }

}
