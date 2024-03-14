using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    [SerializeField] GameObject explosionEffect;
    [SerializeField] float radius;


    AudioManager audioManager;
    bool hasExploded = false;

      void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Expolde()
    {
        if(hasExploded == false)
        {
            GameObject gernadeClone = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            audioManager.PlayGernadeSoundEffect();
            hasExploded = true;
            Destroy(gernadeClone, 0.9f);
            Destroy(gameObject);

        }
        
    }
}
