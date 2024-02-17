using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] GameObject[] enemyPrefebs;
    [SerializeField] [Range(1f, 5f)] float spawnWaitTime = 2f;
    [SerializeField] int numberofPools = 5;
      void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

     IEnumerator SpawnEnemy()
    {
        while (true && numberofPools >= 1)
        {
            Instantiate(enemyPrefebs[0], transform);
            numberofPools--;
            yield return new WaitForSeconds(spawnWaitTime);
        }
        
    }

    
}
