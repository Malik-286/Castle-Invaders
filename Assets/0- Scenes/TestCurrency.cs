using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCurrency : MonoBehaviour
{

    CurrencyManager currencyManager;




      void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
    }

    public void ClaimReward()
    {
        currencyManager.IncreaseGold(50);
    }
}
