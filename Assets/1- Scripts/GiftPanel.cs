using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiftPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeCountText;
    [SerializeField] float timeForReward = 500f;
    [SerializeField] GameObject coinsAnimationPanel;


   
    void Update()
    {
        timeForReward -= Time.deltaTime;
        timeCountText.text  = timeForReward.ToString("00:00:00");
    }


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
