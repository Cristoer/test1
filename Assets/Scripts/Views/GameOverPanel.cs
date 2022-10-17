using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] Text killCountText;
    [SerializeField] Button relifeBtn;
    [SerializeField] Button backBtn;
    [SerializeField] Button restartBtn;
    private void Start()
    {
        relifeBtn.onClick.AddListener(OnClickRelifeBtn);
        backBtn.onClick.AddListener(OnClickBackBtnBtn);
        restartBtn.onClick.AddListener(OnClickRestartBtn);
    }
    public void OnEnter()
    {
        gameObject.SetActive(true);
        killCountText.text = "用尽全力，击杀了"+gameController.killCount+"只僵尸";
    }
    void OnClickRelifeBtn() {
        gameController.Relife();
        OnExit();
    }

    void OnClickRestartBtn() {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    void OnClickBackBtnBtn()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    public void OnExit()
    {
        gameObject.SetActive(false);
    }
}
