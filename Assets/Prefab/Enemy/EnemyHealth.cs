using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 5;
    int current_hit_points;
    Enemy enemy;
    // Start is called before the first frame update

    void OnEnable(){
        current_hit_points = health;
    }
    void Start(){
        enemy = GetComponent<Enemy>();
    }
    void OnParticleCollision(GameObject other){
        ProcessHit();
    }

    void ProcessHit(){
        current_hit_points --;

        if(current_hit_points <= 0){
            enemy.RewardGold();
            gameObject.SetActive(false);
        }
    }
}
