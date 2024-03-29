using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    private bool mbActive = false;
    public bool Active => mbActive;
    private bool mbComplete = false;
    public bool Completed => mbComplete;

    private int mRequireAmount;
    private int mCurrentAmount;

    public void Init(int requireAmount)
    {
        mRequireAmount = requireAmount;
    }

    public void Activate()
    {
        mbActive = true;
    }

    public void AddAmount(int amount)
    {
        mCurrentAmount += amount;

        if(mCurrentAmount >= mRequireAmount)
        {
            mbComplete = true;

            Debug.Log($"Quest completed!");
        }
    }
}
