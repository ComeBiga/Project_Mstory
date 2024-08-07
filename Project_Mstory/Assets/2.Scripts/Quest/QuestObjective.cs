using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestObjective
{
    public enum EType
    {
        KillEnemies,       // �� óġ
        CollectItems,      // ������ ����
        TalkToNPC,         // NPC�� ��ȭ
        ReachLocation      // Ư�� ��ġ ����
    }

    public EType Type => mType;
    public bool IsCompleted => mIsCompleted;

    private EType mType; // ��ǥ�� ����
    private int mRequiredAmount;        // ��ǥ �޼��� ���� �ʿ��� ���� (�� óġ, ������ ���� ��)
    private int mCurrentAmount;         // ������� �޼��� ����
    private bool mIsCompleted;          // ��ǥ �Ϸ� ����

    public void Init(EType type, int requiredAmount)
    {
        mType = type;
        mRequiredAmount = requiredAmount;
        mCurrentAmount = 0;
        mIsCompleted = false;
    }

    // ��ǥ �ϷḦ üũ�ϴ� �޼���
    public void CheckCompletion()
    {
        if (mCurrentAmount >= mRequiredAmount)
        {
            mIsCompleted = true;
        }
    }

    // ��ǥ�� ������Ʈ�ϴ� �޼��� (������ ������ ����, óġ�� ���� �� ��)
    public void UpdateProgress(int amount)
    {
        mCurrentAmount += amount;

        if (mCurrentAmount >= mRequiredAmount)
        {
            mIsCompleted = true;
        }
    }
}
