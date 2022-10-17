using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBox : MonoBehaviour
{
    [SerializeField] Text coinText;
    void Start()
    {
        coinText.text = GameMgr.instance.player.coin + "";
        GameMgr.instance.AddCoinEvent += OnUpdateCoin;
        GameMgr.instance.SubCoinEvent += OnUpdateCoin;

    }

    void OnUpdateCoin(int coin)
    {
        coinText.text = GameMgr.instance.player.coin + "";
    }
    private void OnDestroy()
    {
        GameMgr.instance.AddCoinEvent -= OnUpdateCoin;
        GameMgr.instance.SubCoinEvent -= OnUpdateCoin;
    }
}
