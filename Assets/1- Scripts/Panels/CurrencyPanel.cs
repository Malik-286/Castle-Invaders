using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyPanel : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI goldcurrencyText;
    [SerializeField] TextMeshProUGUI diamondcurrencyText;
     void Start()
    {

    }

    void Update()
    {
        UpdateGoldCurrencyText();
        UpdateDiamondCurrencyText();
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

    void UpdateDiamondCurrencyText()
    {
        if (CurrencyManager.Instance)
        {
            int currentDiamond = CurrencyManager.Instance.GetCurrentDiamond();
            diamondcurrencyText.text = currentDiamond.ToString();
        }
    }

}
