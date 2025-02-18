using Steamworks;
using UnityEngine;

public class SteamSettings : MonoBehaviour
{
    [SerializeField] string testName;

    void Start()
    {
        if (!SteamManager.Initialized)
        {
            return;
        }
        string name = SteamFriends.GetPersonaName();
        testName = name;
        Debug.Log(testName);
    }

     
}
