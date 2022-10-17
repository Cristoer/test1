using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBtn : MonoBehaviour
{
    [SerializeField] Stars stars;
    [SerializeField] Image bgImage;
    [SerializeField] Image levelImage;


    string color = "#727272";
    public System.Action<int> OnClickEvent;
    int _index;
    public void Init(int index) {
        _index = index;
        GetComponent<Button>().onClick.AddListener(OnClickSelf);
    }
    public void SetData(int starCount,bool isGray) {
        stars.SetLightCount(starCount);
        ColorUtility.TryParseHtmlString(color, out Color grayColor);
        if (isGray)
        {
            if (_index == GameMgr.instance.player.levelStars.Count&&GameMgr.instance.StarIsEnough(_index)) {
                bgImage.color = Color.white;
                levelImage.color = Color.white;
                return;
            }
            bgImage.color = grayColor;
            levelImage.color = grayColor;
        }
        else {
            bgImage.color = Color.white;
            levelImage.color = Color.white;
        }
    }
    void OnClickSelf() {
        OnClickEvent?.Invoke(_index);
    }
   
   
}
