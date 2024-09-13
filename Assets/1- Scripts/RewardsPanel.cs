using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsPanel : MonoBehaviour
{

    public GameObject coinsAnimationPanel;
    public GameObject jemsAnimationPanel;

    CurrencyManager currencyManager;

    void OnEnable()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();

        coinsAnimationPanel.SetActive(false);
        jemsAnimationPanel.SetActive(false);
    }

    public IEnumerator IncreaseGoldCurrency()
    {
        yield return new WaitForSeconds(2.2f);
        currencyManager.IncreaseGold(100);
        currencyManager.SaveCurrencyData();

    }

    public IEnumerator IncreaseDiamondCurrency()
    {
        yield return new WaitForSeconds(2.2f);
        currencyManager.IncreaseDiamond(5);
        currencyManager.SaveCurrencyData();

    }

    public void DeActivateCoinAnimationPanel()
    {
        coinsAnimationPanel.SetActive(false);
    }

    public void DeActivateJemsAnimationPanel()
    {
        jemsAnimationPanel.SetActive(false);
    }

    public void ActivateCoinAnimationPanel()
    {
        coinsAnimationPanel.SetActive(true);
    }

    public void ActivateJemsAnimationPanel()
    {
        jemsAnimationPanel.SetActive(true);
    }
}
