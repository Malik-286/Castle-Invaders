using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;
using System;

public class SteamProfilePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userName;
    [SerializeField] private Image playerIcon;

    private void Start()
    {
        if (!SteamManager.Initialized)
        {
            Debug.LogWarning("Steam is not initialized");
            return;
        }

        LoadSteamProfile();
    }

    private void LoadSteamProfile()
    {
        // Username
        userName.text = SteamFriends.GetPersonaName();

        // Avatar
        int imageId = SteamFriends.GetLargeFriendAvatar(SteamUser.GetSteamID());

        if (imageId == -1)
        {
            // Avatar not ready yet (Steam still loading)
            Invoke(nameof(LoadSteamProfile), 0.5f);
            return;
        }

        if (imageId == 0)
        {
            Debug.LogWarning("Steam avatar not available");
            return;
        }

        playerIcon.sprite = GetSteamAvatarAsSprite(imageId);
    }

    private Sprite GetSteamAvatarAsSprite(int imageId)
    {
        uint width, height;
        SteamUtils.GetImageSize(imageId, out width, out height);

        byte[] imageData = new byte[width * height * 4];
        SteamUtils.GetImageRGBA(imageId, imageData, (int)(width * height * 4));

        Texture2D texture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);
        texture.LoadRawTextureData(imageData);
        texture.Apply();

        return Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );
    }
}
