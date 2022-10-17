using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [SerializeField] SelectPanel selectPanel;
    [SerializeField] AddCoinPanel addCoinPanel;
    [SerializeField] CanvasGroup canvasGroup;
    private void Start()
    {
        addCoinPanel.CloseEvent += OnBackHome;
        if (GameMgr.instance.isVectory) OnClickHuntBtn();
    }


    public void OnClickAddCoinBtn() {
        addCoinPanel.Enter();
        canvasGroup.blocksRaycasts = false;
    }
    public void OnClickHuntBtn() {
        selectPanel.Enter();
    }
    void OnBackHome() {
        canvasGroup.blocksRaycasts = true;
    }
    private void OnDestroy()
    {
        selectPanel.CloseEvent -= OnBackHome;
    }
}
