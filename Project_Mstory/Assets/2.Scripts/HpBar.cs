using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    private Image imgHpBar;
    [SerializeField]
    private Sprite[] sprtHpBars;
    [SerializeField]
    private Sprite sprtHpBarMax;

    private int mMax;

    public void Init(int max)
    {
        mMax = max;
    }

    public void Set(int current)
    {
        if(current == mMax)
        {
            imgHpBar.sprite = sprtHpBarMax;

            return;
        }

        float hpRate = (float)current / mMax;

        int hpTypeMaxIndex = sprtHpBars.Length - 1;
        float hpTypeRate = hpTypeMaxIndex * hpRate;

        int hpTypeNumber = Mathf.CeilToInt(hpTypeRate);

        imgHpBar.sprite = sprtHpBars[hpTypeNumber];

        Debug.Log($"hpTypeNumber : {hpTypeNumber}, hpRate : {hpRate}, hpTypeRate : { hpTypeRate }");
    }
}
