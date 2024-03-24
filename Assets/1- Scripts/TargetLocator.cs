using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocater : MonoBehaviour
{

    [SerializeField] GameObject weapon;
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem towerShootingParticles;

    Transform target;
 BattleManager battleManager;
 
      void Start()
    {
 
        battleManager = FindObjectOfType<BattleManager>();
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
       


    }


    void AimWeapon()
    {
        if (target == null)
        {
            Attack(false); // No target in range, stop shooting
            return;
        }

        float targetDistance = Vector3.Distance(transform.position, target.position);
  

        weapon.transform.LookAt(target);

        if (targetDistance < range)
        {
            Attack(true); // Target within range, start shooting
         }
        else if (targetDistance > range)
        {
            Attack(false); // Target out of range, stop shooting
        }
    }

    void Attack(bool isActive)
    {

        var emissionModule = towerShootingParticles.emission;
        if (isActive)
        {
            if (!emissionModule.enabled)
            {
                emissionModule.enabled = true; // Enable particle emission

            }
        }
        else
        {
            emissionModule.enabled = false; // Disable particle emission
        }

    }

    void OnDrawGizmos()
    {
         Gizmos.color = Color.green;
 
        Gizmos.DrawWireSphere(transform.position, range);
    }

}



