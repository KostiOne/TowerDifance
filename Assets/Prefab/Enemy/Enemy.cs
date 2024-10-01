using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int reward = 25;
    [SerializeField] int goldPenalty = 5;

    Bank bank;
    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold(){
        if(bank == null) {return;}
        bank.AddMoney(reward);
    }

     public void PenaltyApply(){
       // Debug.Log("1");
        if(bank == null) {
            Debug.Log("1");
        }
        bank.BuyTower(goldPenalty);
    }
}
