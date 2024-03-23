using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiftPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeCountText;
    [SerializeField] GameObject coinsAnimationPanel;


   
  

    public void ActivateCoinsAnimationPanel()
    {
       coinsAnimationPanel.SetActive(true);
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayCoinsCollectionSoundEffect();

            StartCoroutine(DeactivateRewardsPanel());
        }
    }
    
        



    IEnumerator DeactivateRewardsPanel()
    {
         
        yield return new WaitForSeconds(1.2f);
        this.gameObject.SetActive(false);
    }

   

}
