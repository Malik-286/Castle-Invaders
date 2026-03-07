using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyPanel : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI goldcurrencyText;
   
    void Update()
    {
        UpdateGoldCurrencyText();
    }

   

    void UpdateGoldCurrencyText()
    {
        if (CurrencyManager.Instance)
        {
            int currentGold = CurrencyManager.Instance.GetCurrentGold();
            if (currentGold >= 1000)
            {
                float goldInK = currentGold / 1000f;
                goldcurrencyText.text = goldInK.ToString("0.#") + "k";
            }
            else
            {
                goldcurrencyText.text = currentGold.ToString();
            }
            if (currentGold < 0)
            {
                currentGold = 0;
            }
        }
    }

     

}
