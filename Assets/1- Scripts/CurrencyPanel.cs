using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyPanel : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI currencyText;
  
    CurrencyManager currencyManager;
     void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
    }

    void Update()
    {
        UpdateCurrencyText();
    }

    void UpdateCurrencyText()
    {

        int currentGold = currencyManager.GetCurrentGold();

        if (currentGold >= 1000)
        {
            float goldInK = currentGold / 1000f;
            currencyText.text = goldInK.ToString("0.#") + "k";
        }
        else
        {
            currencyText.text = currentGold.ToString();
        }
    }
}
