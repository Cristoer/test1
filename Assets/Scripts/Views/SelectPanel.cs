using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectPanel : MonoBehaviour
{
    public Action CloseEvent;
    [SerializeField] LevelBtn[] levelBtns;
    [SerializeField] Tip tip;
    [SerializeField] CanvasGroup canvasGroup;
    private void Awake()
    {
        for (int i = 0; i < levelBtns.Length; i++) {
            LevelBtn levelBtn = levelBtns[i];
            levelBtn.OnClickEvent += OnClickSelectLevel;
            levelBtn.Init(i);
        }
        GameMgr.instance.SelectLevelCompleteEvent += OnSelectLevelComplete;
    }
    void OnSelectLevelComplete()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void Enter() {
        gameObject.SetActive(true);
        List<int> levelStars = GameMgr.instance.player.levelStars;
        for (int i = 0; i < levelBtns.Length; i++) {
            if (i < levelStars.Count)
            {
                levelBtns[i].SetData(levelStars[i],false);
            }
            else {
                levelBtns[i].SetData(0,true);
            }
        }
    }

    public void OnClickSelectLevel(int levelIndex) {
        GameMgr.instance.SelectLevel(levelIndex,()=> {
            tip.Show("Need more stars");
        });
    }
    public void Exit() {
        CloseEvent?.Invoke();
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        GameMgr.instance.SelectLevelCompleteEvent -= OnSelectLevelComplete;
        for (int i = 0; i < levelBtns.Length; i++)
        {
            levelBtns[i].OnClickEvent += OnClickSelectLevel;
        }
    }
}
