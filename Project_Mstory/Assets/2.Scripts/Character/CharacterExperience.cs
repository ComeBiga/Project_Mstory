using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExperience
{
    public int Max => mMax;
    public int Current => mCurrent;

    public Action onFull = null;

    private int mMax;
    private int mCurrent;

    public void Init(int max)
    {
        mMax = max;
        mCurrent = 0;
    }

    public void Increase(int amount)
    {
        mCurrent += amount;

        if(mCurrent >= mMax)
        {
            mCurrent = 0;

            onFull?.Invoke();
        }
    }

    public void Next(int max)
    {
        Init(max);
    }
}
