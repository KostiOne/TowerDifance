using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyInstantiation : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0,50)]int  poolSize = 5;
    [SerializeField] [Range(0.1f,30f)]float spawnTimer = 1f;

    GameObject[] pool;

    void Awake(){
        if(poolSize <= 0) {
            Debug.LogError("Pool size must be greater than 0!");
            return;
        }
        PopulatePool();
    }
    void Start()
    {
        StartCoroutine(InstantiateAnEnemy());
    }

    void PopulatePool(){
        pool = new GameObject[poolSize];

        for(int i = 0; i  < pool.Length; i++){
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool(){
        for(int i = 0; i  < pool.Length; i++){
            if(pool[i].activeInHierarchy == false){
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator InstantiateAnEnemy(){
        while(true){
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
