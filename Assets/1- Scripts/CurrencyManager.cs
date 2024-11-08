using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>  
{

    [SerializeField] int currentGold;
    [SerializeField] int currentDiamond;


    const int defaultGold = 50;
    const int defaultdiamond = 5;


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
        if (currentDiamond <= 0)
        {
             SaveCurrencyData();
        }
    }

    void Update()
    {
        if (currentGold <= 0)
        {
            currentGold = 0;
        }

        currentGold = (int)Mathf.Clamp(currentGold, 0, Mathf.Infinity);
        currentDiamond = (int)Mathf.Clamp(currentDiamond, 0, Mathf.Infinity);


    }


    public int GetCurrentGold()
    {    
        return currentGold;
    }
    public int GetCurrentDiamond()
    {
        return currentDiamond;

    }

    public void IncreaseGold(int amountToIncrease)
    {
        currentGold += amountToIncrease;
        SaveCurrencyData();
    }

    public void IncreaseDiamond(int amountToIncrease)
    {
        currentDiamond += amountToIncrease;
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

    public void DecreaseDiamond(int amountToDecrease)
    {
        if (currentDiamond <= 0)
        {
            Debug.Log("Not Enough Diamond");
            return;
        }
        currentDiamond -= amountToDecrease;
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
        this.currentDiamond = data.diamond;
    }




     
}
