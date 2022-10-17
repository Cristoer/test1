using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Tip : MonoBehaviour
{
    [SerializeField] Text contentText;
    [SerializeField] RectTransform rectTransform;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void Show(string content,System.Action complete=null) {
        gameObject.SetActive(true);
        contentText.text = content;
        rectTransform.anchoredPosition = new Vector3(0, -500, 0);
        rectTransform.DOAnchorPosY(0, 1f, false).OnComplete(()=> {
            gameObject.SetActive(false);
            complete?.Invoke();
        });
    }
}
