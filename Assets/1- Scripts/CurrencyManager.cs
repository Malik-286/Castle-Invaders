using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>  
{

    [SerializeField] int currentGold;
    [SerializeField] const int defaultGold = 50;


    protected override void Awake()
    {
        base.Awake();
        LoadCurrencyData();
    }

    void Start()
    {
        if (currentGold <= 0)
        {
            currentGold = defaultGold;
            SaveCurrencyData();
        }
    }


    public int GetCurrentGold()
    {
        if(currentGold <= 0)
        {
            currentGold = 0;         
        }
        return currentGold;

    }

    public void IncreaseGold(int amountToIncrease)
    {
        currentGold += amountToIncrease;
        SaveCurrencyData();
    }


    public void  DecreaseGold(int amountToDecrease)
    {
        if(currentGold <= 0)
        {
            Debug.Log("Not Enough Gold");
            return;
        }
        currentGold -= amountToDecrease;
        SaveCurrencyData(); 
    }


    public void SaveCurrencyData()
    {
        SaveSystem.SaveData(this);
    }

    public void LoadCurrencyData()
    {
        Data data = SaveSystem.LoadData();
        this.currentGold = data.gold;
    }
}
