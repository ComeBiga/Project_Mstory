using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public int ID => mID;

    private int mID;
    private List<QuestObjective> mObjectives = new List<QuestObjective>();
    private QuestReward mReward;
    private int mRequireAmount;
    private int mCurrentAmount;

    public void Init(int ID, List<QuestObjective> objectives, QuestReward reward)
    {
        mID = ID;
        mObjectives = objectives;
        mReward = reward;
    }

    public void UpdateProgress(QuestObjective.EType objectiveType, int amount)
    {
        foreach (QuestObjective objective in mObjectives)
        {
            if (objective.Type != objectiveType)
                continue;

            objective.UpdateProgress(amount);
        }
    }

    public bool IsCompleted()
    {
        foreach(QuestObjective objective in mObjectives)
        {
            if (!objective.IsCompleted)
            {
                return false;
            }
        }

        return true;
    }
}
