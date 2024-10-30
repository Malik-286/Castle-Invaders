using UnityEngine;

public class DefencePowerManager : MonoBehaviour
{
    public static DefencePowerManager instance;

     private readonly string[] towerNames = { "Tower1", "Tower2", "Tower3", "Tower4", "Tower5" };

     public float totalTowersInScene = 0;

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

    void Start()
    {
         GetTotalTowersInSceneValue();
    }

    public float GetTotalTowersInSceneValue()
    {
        totalTowersInScene = 0;  

        foreach (string towerName in towerNames)
        {
            GameObject[] towers = GameObject.FindGameObjectsWithTag(towerName);
            totalTowersInScene += towers.Length; 
        }

        return totalTowersInScene;  
    }
}
