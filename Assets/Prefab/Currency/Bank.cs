using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI balance;
    [SerializeField] int startAmount = 150;
    [SerializeField ]int currentBalance = 0;
    public int CurrentBalance {get { return currentBalance; } }

    void Awake(){
        currentBalance += startAmount;
        //balance = GetComponent<TextMeshProUGUI>();
        UpdateUICurrency();
    }

    public void AddMoney(int amount){
        currentBalance += Mathf.Abs(amount);
        UpdateUICurrency();
    }

    public void BuyTower(int amount){
        currentBalance -= Mathf.Abs(amount);
        UpdateUICurrency();
        if(currentBalance < 0){
           //LooseRame
        }
    }

    void UpdateUICurrency(){
        balance.text = "Balacne: " + currentBalance;
    }
    void ReloadScene(){
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
