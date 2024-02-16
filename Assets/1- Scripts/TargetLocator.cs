using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocater : MonoBehaviour
{

    [SerializeField] GameObject weapon;
     Transform target;
     void Start()
    {
        target = FindObjectOfType<EnemyMovement>().transform;
    }

     void Update()
    {
        AimWeapon();
    }

    void AimWeapon()
    {
        transform.LookAt(target);
    }
}
