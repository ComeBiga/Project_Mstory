using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Quest mQuest;

    public void InteractTo(Player player)
    {
        if (mQuest.Active)
        {
            if(mQuest.Completed)
            {
                GiveRewardTo(player);
            }
        }
        else
        {
            GiveQuestTo(player);
        }
    }

    private void Start()
    {
        mQuest = new Quest();
        mQuest.Init(1);
    }

    private void GiveQuestTo(Player player)
    {
        mQuest.Activate();
        player.AddQuest(mQuest);

        Debug.Log($"NPC gives quest to player!");
    }

    private void GiveRewardTo(Player player)
    {
        Debug.Log($"NPC gives reward to player!");
    }
}
