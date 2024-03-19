using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocater : MonoBehaviour
{

    [SerializeField] GameObject weapon;
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem towerShootingParticles;
     
    Transform target;

    AudioManager audioManager;


      void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }



    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            // Check if the enemy is on a different layer than the tower
            if (enemy.gameObject.layer != gameObject.layer)
            {
                float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
                if (targetDistance < maxDistance)
                {
                    closestTarget = enemy.transform;
                    maxDistance = targetDistance;
                }
            }
        }

        target = closestTarget;

        if (target != null)
        {
            Debug.Log("Found target: " + target.name + ", distance: " + Vector3.Distance(transform.position, target.position));
        }
        else
        {
            Debug.Log("No target found.");
        }
    }
  

    void AimWeapon()
    {
        if (target != null)
        {
            float targetDistance = Vector3.Distance(transform.position, target.position);
            weapon.transform.LookAt(target);
            if (targetDistance < range)
            {
                Attack(true);
            }
            else
            {
                Attack(false);
            }
        }
        else
        {
            Attack(false);  
        }
    }

    void Attack(bool isActive)
    {
       

          var emissionModule = towerShootingParticles.emission;
        if(emissionModule.enabled == false)
        {
            emissionModule.enabled = isActive;
        }
             
       
        // destroy tower after 15 seconds
        Destroy(gameObject, 12);
        // play some destroy particle effects

    }
}
