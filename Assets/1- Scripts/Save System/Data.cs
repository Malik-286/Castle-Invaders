
using UnityEngine;

[System.Serializable]
public class Data 
{

    public int gold;
    public Data(CurrencyManager currencyManager)
    {
       this.gold = currencyManager.GetCurrentGold();
    }


}
