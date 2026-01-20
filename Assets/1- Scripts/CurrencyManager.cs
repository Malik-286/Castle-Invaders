using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>  
{

    [SerializeField] int currentGold;
    [SerializeField] int currentDiamond;


    const int defaultGold = 50;
  //  const int defaultdiamond = 1;


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
           //  currentDiamond = defaultdiamond;
             SaveCurrencyData();
        }
    }

    void Update()
    {
        if (currentGold <= -1)
        {
            currentGold = 0;
        }
        if (currentDiamond <= -1)
        {
            currentDiamond = 0;
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
        print("diamond before" + currentDiamond);
        currentDiamond += amountToIncrease;
        print("diamond After" + currentDiamond);
        print("Amount to increase" + amountToIncrease);

        SaveCurrencyData();
    }


    public void  DecreaseGold(int amountToDecrease)
    {
        if(currentGold <= 0)
        {
            Debug.Log("Not Enough Gold");
            currentGold = 0;
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
            currentDiamond = 0;
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
