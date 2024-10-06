using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject TopPath;
    [SerializeField] GameObject BottomPath;
    [SerializeField] int buildDeley = 1;
    [SerializeField] int cost = 50;
    

    void Start(){

        StartCoroutine(Build());
    }


    public bool CreateTower(Tower tower, Vector3 position){
        Bank bank = FindObjectOfType<Bank>();
        if(bank == null){
            return false;
        }

        if(bank.CurrentBalance >= cost){
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.BuyTower(cost);
            return true;
        }
        return false;
    }

    IEnumerator Build(){
        foreach(Transform child in transform){
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child){
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach(Transform child in transform){
            child.gameObject.SetActive(true);
             yield return new WaitForSeconds(buildDeley);
            foreach(Transform grandchild in child){
                grandchild.gameObject.SetActive(true);
            }
        }


    }
}
