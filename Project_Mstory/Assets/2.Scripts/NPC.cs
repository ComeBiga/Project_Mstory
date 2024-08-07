using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Quest mQuest;

    public void Interact()
    {
        if (QuestManager.instance.IsActivated(mQuest))
        {
            if(mQuest.IsCompleted())
            {
                GiveRewardTo();
            }
        }
        else
        {
            GiveQuestTo();
        }
    }

    private void Start()
    {
        //mQuest = new Quest();
        //mQuest.Init(1);

        var newQuest = new Quest();

        var newObjective = new QuestObjective();
        newObjective.Init(QuestObjective.EType.KillEnemies, 1);

        var questObjectives = new List<QuestObjective>();
        questObjectives.Add(newObjective);

        var questReward = new QuestReward();
        questReward.exp = 3;

        newQuest.Init(0, questObjectives, questReward);

        mQuest = newQuest;
    }

    private void GiveQuestTo()
    {
        QuestManager.instance.ActivateQuest(mQuest);

        Debug.Log($"NPC gives quest to player!");
    }

    private void GiveRewardTo()
    {
        Debug.Log($"NPC gives reward to player!");
        QuestManager.instance.GiveReward(mQuest);
    }
}
