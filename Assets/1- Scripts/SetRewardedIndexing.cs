 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRewardedIndexing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetIndex(int Tellme)
    {
        if (AdmobRewardedVideo.Instance)
        {
            AdmobRewardedVideo.Instance.Index = Tellme;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

