using UnityEngine;
using TMPro;

public class SteamProfilePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI userName;
    [SerializeField] TextMeshProUGUI playerCurrentLevel;

 
    

    void Update()
    {
        if (SteamSettings.instance != null)
        {
            userName.text = SteamSettings.instance.userName;
        }
        else
        {
            Debug.LogWarning("SteamSettings instance is null");
        }
    }

     
}
