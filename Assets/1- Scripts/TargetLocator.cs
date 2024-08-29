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
        if (this.gameObject.CompareTag("Tower3"))
        {
            CheckIfAnyEnemyisAlive();
        }

    }


    public void CheckIfAnyEnemyisAlive()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            // Enemies are present, start emitting particles
            if (!towerShootingParticles.isPlaying)
                towerShootingParticles.Play();
            Debug.Log("There are enemies");
        }
        else
        {
            // No enemies, stop emitting particles
            if (towerShootingParticles.isPlaying)
                towerShootingParticles.Stop();
            Debug.Log("There are no enemies");
        }
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

    IEnumerator PlayTowerShootingSoundEffect(AudioClip audioClip, float volume)
    {
         
           
          Debug.Log("Playing Shooting Audio");
         yield return new WaitForSeconds(2f); 
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, range);
    }

}


