using Steamworks;
using UnityEngine;

public class SteamSettings : MonoBehaviour
{
    public static SteamSettings instance;

    public string userName;

    void Awake()
    {
        // Make sure there is only one instance of SteamSettings
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: to keep it across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    void Start()
    {
        if (!SteamManager.Initialized)
        {
            return;
        }
        string name = SteamFriends.GetPersonaName();
        this.userName = name;
        Debug.Log(userName);
    }
}

