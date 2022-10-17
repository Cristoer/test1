using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] Text[] propsTexts;
    [SerializeField] Text killCountText;
    [SerializeField] Text hpText;
    [SerializeField] GamePassPanel gamePassPanel;
    [SerializeField] GameOverPanel gameOverPanel;
    [SerializeField] PausePanel pausePanel;
    [SerializeField] GameConfig gameConfig;
    GameConfig.LevelConfig levelConfig;
    [SerializeField] RectTransform[] starTransforms;
    [SerializeField] Slider killCountSlide;
    [SerializeField] Stars stars;
    [SerializeField] CanvasGroup canvasGroup;
    private void Awake()
    {
        gameController.GetPropsEvent += OnGetProps;
        gameController.UsePropsEvent += OnUseProps;
        gameController.KillZombieEvent += OnKillZombie;
        gameController.PlayerUpdateHpEvent += OnPlayerUpdateHp;
        gameController.GameOverEvent += OnGameOver;
        gameController.GamePassEvent += OnGamePass;
        gameController.StartGameEvent += OnStartGame;
        levelConfig = gameConfig.levelConfigs[GameMgr.instance.selectLevel];
        OnKillZombie();
        stars.SetLightCount(0);
        InitStarPosition(levelConfig.starNeedkillCount);
    }
    public void OnClickPauseGameBtn() {
        gameController.PauseGame();
        pausePanel.OnEnter();
        canvasGroup.blocksRaycasts = false;
    }
    void OnStartGame() {
        canvasGroup.blocksRaycasts = true;
    }
    void InitStarPosition(int[] starCountKillArr) {
        int count = starCountKillArr.Length;
        int maxNeedCount = starCountKillArr[count - 1];
        float width = (killCountSlide.transform as RectTransform).sizeDelta.x;
        for (int i = 0; i < count; i++)
        {
            float proper= (float)starCountKillArr[i] / maxNeedCount;
            starTransforms[i].anchoredPosition =new Vector2(-width / 2 + proper * width,0);
        }
    }
    void OnGameOver() {
        gameOverPanel.OnEnter();
        canvasGroup.blocksRaycasts = false;
    }
    void OnGamePass() { 
        gamePassPanel.OnEnter();
        canvasGroup.blocksRaycasts = false;
    }
    void OnGetProps(PropsType propsType) {
        int propsIndex = (int)propsType;
        propsTexts[propsIndex].text = gameController.propsCounts[propsIndex]+"";
    }
    void OnUseProps(PropsType propsType)
    {
        int propsIndex = (int)propsType;
        propsTexts[propsIndex].text = gameController.propsCounts[propsIndex] + "";
    }
    void OnKillZombie() {
        killCountText.text ="killCount:"+ gameController.killCount ;
      
        killCountSlide.value = (float)gameController.killCount / levelConfig.starNeedkillCount[2];
        stars.SetLightCount(GameMgr.instance.GetStarCount(gameController.killCount));
    }
    public void UseProps(int propsType) {
        gameController.UseProp((PropsType)propsType);
    }
    void OnPlayerUpdateHp(int hp) {
        hpText.text ="hp:"+ hp;
    }
    private void OnDestroy()
    {
        gameController.GetPropsEvent -= OnGetProps;
        gameController.UsePropsEvent -= OnUseProps;
        gameController.KillZombieEvent -= OnKillZombie;
        gameController.PlayerUpdateHpEvent -= OnPlayerUpdateHp;
        gameController.GameOverEvent-= OnGameOver;
        gameController.GamePassEvent -= OnGamePass;
        gameController.StartGameEvent -= OnStartGame;
    }

}
