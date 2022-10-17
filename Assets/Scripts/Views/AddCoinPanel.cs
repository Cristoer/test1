using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AddCoinPanel : MonoBehaviour
{
    public Action CloseEvent;
    [SerializeField] Tip tip;
    void OnAddCoin(int coin) {
        tip.Show("Add " + coin + " coin");
    }
    public void Enter()
    {
        gameObject.SetActive(true);
        GameMgr.instance.AddCoinEvent += OnAddCoin;
    }

    public void OnClickAdAddCoin() {
        GameMgr.instance.AdAddCoin();
    }
    public void Exit()
    {
        CloseEvent?.Invoke();
        gameObject.SetActive(false);
        GameMgr.instance.AddCoinEvent -= OnAddCoin;
    }
}
