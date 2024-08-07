using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestObjective
{
    public enum EType
    {
        KillEnemies,       // 적 처치
        CollectItems,      // 아이템 수집
        TalkToNPC,         // NPC와 대화
        ReachLocation      // 특정 위치 도달
    }

    public EType Type => mType;
    public bool IsCompleted => mIsCompleted;

    private EType mType; // 목표의 유형
    private int mRequiredAmount;        // 목표 달성을 위해 필요한 수량 (적 처치, 아이템 수집 등)
    private int mCurrentAmount;         // 현재까지 달성된 수량
    private bool mIsCompleted;          // 목표 완료 여부

    public void Init(EType type, int requiredAmount)
    {
        mType = type;
        mRequiredAmount = requiredAmount;
        mCurrentAmount = 0;
        mIsCompleted = false;
    }

    // 목표 완료를 체크하는 메서드
    public void CheckCompletion()
    {
        if (mCurrentAmount >= mRequiredAmount)
        {
            mIsCompleted = true;
        }
    }

    // 목표를 업데이트하는 메서드 (수집된 아이템 수량, 처치한 적의 수 등)
    public void UpdateProgress(int amount)
    {
        mCurrentAmount += amount;

        if (mCurrentAmount >= mRequiredAmount)
        {
            mIsCompleted = true;
        }
    }
}
