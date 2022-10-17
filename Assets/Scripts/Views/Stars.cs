using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stars : MonoBehaviour
{
    [SerializeField] RawImage[] starArr;
    [SerializeField] Texture lightStarTex;
    [SerializeField] Texture grayStarTex;
    int currentCount=-1;
    public void SetLightCount(int count) {
        if (currentCount == count) return;
        currentCount = count;
        for (int i = 0; i < starArr.Length; i++) {
            if (i < count) {
                starArr[i].texture = lightStarTex;
            }
            else
            {
                starArr[i].texture = grayStarTex;
            }
        }
    }
}
