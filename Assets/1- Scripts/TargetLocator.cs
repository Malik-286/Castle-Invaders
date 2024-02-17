using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocater : MonoBehaviour
{

    [SerializeField] GameObject weapon;
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem towerShootingParticles;
    Transform target;
    

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }



    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistancce = Mathf.Infinity;

        foreach (Enemy enemy in enemies) 
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistancce)
            {
                closestTarget = enemy.transform;
                maxDistancce = targetDistance;
            }
        }
            target = closestTarget;

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
        emissionModule.enabled = isActive;
        
    }
}
