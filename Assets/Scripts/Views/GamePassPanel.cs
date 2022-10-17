using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePassPanel : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] Text killCountText;
    [SerializeField] Text rewardGoldText;
    [SerializeField] Button doubleGoldBtn;
    [SerializeField] GameConfig gameConfig;
    [SerializeField] Button backBtn;
    [SerializeField] Stars stars;
    private void Start()
    {
        doubleGoldBtn.onClick.AddListener(delegate {
            GameMgr.instance.GetDoubleReward(gameController.killCount);
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        });
        backBtn.onClick.AddListener(delegate {
            GameMgr.instance.GetReward(gameController.killCount);
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        });
    }
    public void OnEnter() {
        gameObject.SetActive(true);
        killCountText.text = "本次击杀了" + gameController.killCount + "只僵尸";
        int starCount= GameMgr.instance.GetStarCount(gameController.killCount);
        rewardGoldText.text =gameController.killCount*gameConfig.coinFactor+ "";
        stars.SetLightCount(starCount) ;
    }
    public void OnExit() {
        gameObject.SetActive(false);
    }
}
