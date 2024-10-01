using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingEnemy : MonoBehaviour
{

    [SerializeField] Transform weapon;
    [SerializeField] Transform target;
    [SerializeField] ParticleSystem arrows;
    [SerializeField] [Range(0f,50f)] float enemyRange = 20f; 

    
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();

    }

    void FindClosestTarget(){
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistanse = Mathf.Infinity;
        foreach(Enemy enemy in enemies){
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if(targetDistance < maxDistanse){
                closestTarget = enemy.transform;
                maxDistanse = targetDistance;
            }
        }
        target = closestTarget;
    }
    void AimWeapon(){
        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);

        if(targetDistance < enemyRange){
            Attack(true);
        }else{Attack(false);}
    }

    void Attack(bool isActive){
            var emissionModule = arrows.emission;
            emissionModule.enabled = isActive;
    }
}
