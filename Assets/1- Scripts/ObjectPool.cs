using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] GameObject[] enemyPrefebs;
    [SerializeField] [Range(1f, 5f)] float spawnWaitTime = 2f;
     void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

     IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemyPrefebs[0], transform);
            yield return new WaitForSeconds(spawnWaitTime);
        }
        
    }

    
}
