using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsAnimationPanel : MonoBehaviour
{
    public GameObject[] coins;
    public Transform targetPosition;
    public float movementSpeed = 1500;

     bool isMoving = false;

    void Start()
    {
         StartCoinMovement();
    }

    void Update()
    {
         if (isMoving)
        {
            foreach (GameObject coin in coins)
            {
                coin.transform.position = Vector3.MoveTowards(coin.transform.position, targetPosition.position, movementSpeed * Time.deltaTime);
            }
        }
    }

    void StartCoinMovement()
    {
         isMoving = true;
        
    }

    
}
