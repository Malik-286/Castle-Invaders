using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiftPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeCountText;
    [SerializeField] float timeForReward = 5000f;
     void Update()
    {
        timeForReward -= Time.deltaTime;
        timeCountText.text  = timeForReward.ToString("00:00:00");
    }
}
