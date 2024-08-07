using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance = null;

    List<Quest> activeQuests = new List<Quest>();

    public void ActivateQuest(Quest quest)
    {
        activeQuests.Add(quest);
    }

    public bool IsActivated(Quest quest)
    {
        return activeQuests.Contains(quest);
    }

    public void UpdateQuestObjectives(int questID, QuestObjective.EType questType, int amount)
    {
        Quest quest = activeQuests.Find(q => q.ID == questID);

        if (quest != null)
        {
            quest.UpdateProgress(questType, amount);
        }
    }

    public void GiveReward(Quest quest)
    {
        // 보상 지급 로직
        activeQuests.Remove(quest);
    }

    private void Awake()
    {
        instance = this;
    }
}
