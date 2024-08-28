
using UnityEngine;

[System.Serializable]
public class Data 
{

    public int gold;
    public int diamond;
    public Data(CurrencyManager currencyManager)
    {
       this.gold = currencyManager.GetCurrentGold();
        this.diamond = currencyManager.GetCurrentDiamond();
    }


}
