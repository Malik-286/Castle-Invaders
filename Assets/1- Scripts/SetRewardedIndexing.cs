using System;
using UnityEngine;


public class SetRewardedIndexing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetIndex(int Tellme)
    {
        print("Rewarded Ad Index is " + Tellme);

        if (AdmobRewardedVideo.Instance)
        {
        print(" Clicking Rewarded Ad Index is " + AdmobRewardedVideo.Instance.Index);
            AdmobRewardedVideo.Instance.Index = Tellme;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
