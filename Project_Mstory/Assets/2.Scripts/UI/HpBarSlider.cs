using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarSlider : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    private int mMax;
    // private int mCurrent;

    public void Init(int max)
    {
        mMax = max;
    }

    public void Set(int current)
    {
        _slider.value = (float)current / mMax;
    }
}
