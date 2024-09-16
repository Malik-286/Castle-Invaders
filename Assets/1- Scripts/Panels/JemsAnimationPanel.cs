using hardartcore.CasualGUI;
using System.Collections;
using UnityEngine;

public class JemssAnimationPanel : MonoBehaviour
{
    public GameObject[] jems;
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
        originalPositions = new Vector3[jems.Length];
        originalScales = new Vector3[jems.Length];

        for (int i = 0; i < jems.Length; i++)
        {
            if (jems[i] != null)
            {
                jems[i].SetActive(true); // Make coins visible again
                originalPositions[i] = jems[i].transform.position;
                originalScales[i] = jems[i].transform.localScale;
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
        for (int i = 0; i < jems.Length; i++)
        {
            GameObject coin = jems[i];
            StartCoroutine(MoveJems(coin, i));
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

    IEnumerator MoveJems(GameObject jem, int index)
    {
        if (jem == null) yield break;

        while (jem != null && Vector3.Distance(jem.transform.position, targetPosition.position) > 0.1f)
        {
            // Move the coin towards the target position
            jem.transform.position = Vector3.MoveTowards(jem.transform.position, targetPosition.position, movementSpeed * Time.deltaTime);

            // Scale down the coin
            jem.transform.localScale = Vector3.Lerp(jem.transform.localScale, Vector3.one * minScale, Time.deltaTime * 2); // Smooth scaling

            yield return null; // Wait for the next frame
        }

        if (jem != null)
        {
            if (audioManager != null && coinSpillSoundEffect != null)
            {
                // Play the sound effect
                audioManager.PlaySingleShotAudio(coinSpillSoundEffect, 1f);
            }

            // Reset the coin's position and scale
            jem.transform.position = originalPositions[index];
            jem.transform.localScale = originalScales[index];

            // Hide the coin after it reaches its destination
            jem.SetActive(false);
        }
    }
}
