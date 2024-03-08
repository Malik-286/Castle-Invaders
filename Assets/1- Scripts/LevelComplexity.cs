using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplexity : MonoBehaviour
{

    [SerializeField] string currentLevelOfComplexity;


    GameManager gameManager;
     void Start()
    {
        gameManager   = FindObjectOfType<GameManager>();
        SetCurrenctLevelOfComplexity();
    }

    void SetCurrenctLevelOfComplexity()
    {
        currentLevelOfComplexity = gameManager.GetCurrentSceneName();
        Debug.Log("Current Level of Game Complexity is "+currentLevelOfComplexity);
    }

     // use switch statements here to set the each game level complexity
}
