using hardartcore.CasualGUI;
using System.Collections;
using UnityEngine;

public class CoinsAnimationPanel : MonoBehaviour
{
    public GameObject[] coins;
    public Transform targetPosition;
    public float movementSpeed = 2000;
    public float minScale = 0.2f; // Minimum scale to shrink to

    public AudioClip coinSpillSoundEffect;

    private AudioManager audioManager;
    private Vector3[] originalPositions;
    private Vector3[] originalScales;

    void OnEnable()
    {
        // Initialize the original positions and scales of the coins
        originalPositions = new Vector3[coins.Length];
        originalScales = new Vector3[coins.Length];

        for (int i = 0; i < coins.Length; i++)
        {
            if (coins[i] != null)
            {
                coins[i].SetActive(true); // Make coins visible again when the panel is re-enabled
                originalPositions[i] = coins[i].transform.position;
                originalScales[i] = coins[i].transform.localScale;
            }
        }

        StartCoroutine(StartCoinMovement());
        audioManager = FindObjectOfType<AudioManager>();

        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene!");
        }
    }

    IEnumerator StartCoinMovement()
    {
        for (int i = 0; i < coins.Length; i++)
        {
            GameObject coin = coins[i];
            StartCoroutine(MoveCoin(coin, i));
            yield return new WaitForSeconds(0.33f); // Delay between each coin movement
        }

        // Wait 1 second after the last coin has moved
        yield return new WaitForSeconds(1f);

        // Deactivate the parent and this game object after all coins have moved
        if (transform.parent != null)
        {
            transform.parent.gameObject.GetComponent<Dialog>().HideDialog();  // Deactivate the parent GameObject
        }
    }

    IEnumerator MoveCoin(GameObject coin, int index)
    {
        if (coin == null) yield break;

        while (coin != null && Vector3.Distance(coin.transform.position, targetPosition.position) > 0.1f)
        {
            // Move the coin towards the target position
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, targetPosition.position, movementSpeed * Time.deltaTime);

            // Scale down the coin
            coin.transform.localScale = Vector3.Lerp(coin.transform.localScale, Vector3.one * minScale, Time.deltaTime * 2); // Smooth scaling

            yield return null; // Wait for the next frame
        }

        if (coin != null)
        {
            if (audioManager != null && coinSpillSoundEffect != null)
            {
                // Play the sound effect
                audioManager.PlaySingleShotAudio(coinSpillSoundEffect, 1f);
            }

            // Reset the coin's position and scale
            coin.transform.position = originalPositions[index];
            coin.transform.localScale = originalScales[index];

            // Hide the coin after it reaches its destination
            coin.SetActive(false);
        }
    }
}

