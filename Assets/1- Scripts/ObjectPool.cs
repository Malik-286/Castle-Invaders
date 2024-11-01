using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] GameObject[] enemyPrefebs;
    [SerializeField] [Range(1f, 20f)] float spawnWaitTime = 2f;
    [SerializeField] int numberofPools = 5;
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    public int GetNumberOfPools()
    {
        return numberofPools;
    }
     IEnumerator SpawnEnemy()
    {
        while (true && numberofPools >= 1)
        {
            yield return new WaitForSeconds(spawnWaitTime);
            Instantiate(enemyPrefebs[0], transform);
            numberofPools--;
        }
        
    }

    
}
