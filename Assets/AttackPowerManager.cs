using UnityEngine;

public class AttackPowerManager : MonoBehaviour
{
    public static AttackPowerManager instance;

       const string enemyTag = "Enemy";

     public float totalEnemiesInScene = 0;

    void Awake()
    {
         if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        GetTotalEnemiesInSceneValue();
    }

    public float GetTotalEnemiesInSceneValue()
    {
        // Find all game objects with the specified enemy tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        totalEnemiesInScene = enemies.Length; // Directly assign the length
        return totalEnemiesInScene;
    }
}

