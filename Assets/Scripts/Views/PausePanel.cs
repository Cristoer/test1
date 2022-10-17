using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PausePanel : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] Button resumeBtn;
    [SerializeField] Button backBtn;
    [SerializeField] Button restartBtn;
    private void Start()
    {
        resumeBtn.onClick.AddListener(OnClickResumeBtn);
        backBtn.onClick.AddListener(OnClickBackBtnBtn);
        restartBtn.onClick.AddListener(OnClickRestartBtn);
    }
    public void OnEnter()
    {
        gameObject.SetActive(true);
    }
    void OnClickResumeBtn()
    {
        gameController.StartGame();
        OnExit();
    }

    void OnClickRestartBtn()
    {
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
