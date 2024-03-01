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
        currencyText.text  = currencyManager.GetCurrentGold().ToString();
    }
}
