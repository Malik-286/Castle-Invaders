using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCurrency : MonoBehaviour
{



    CurrencyManager currencyManager;

      void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
    }
    public void GetFakeCurrency()
    {
        currencyManager.IncreaseGold(1000);
        currencyManager.SaveCurrencyData();
    }
}
