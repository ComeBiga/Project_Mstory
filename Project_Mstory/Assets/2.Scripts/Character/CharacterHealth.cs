using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField]
    private int _max = 10;
    [SerializeField]
    private CharacterAnimation _characterAnimation;
    [SerializeField]
    private HpBar _hpBar;

    private int mCurrent;

    public void TakeDamage(int amount)
    {
        mCurrent -= amount;

        _hpBar?.Set(mCurrent);
        _characterAnimation.Hit();

        if(mCurrent <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        QuestManager.instance.UpdateQuestObjectives(0, QuestObjective.EType.KillEnemies, 1);

        Destroy(this.gameObject);
    }

    private void Start()
    {
        mCurrent = _max;

        _hpBar?.Init(_max);
        _hpBar?.Set(_max);
    }
}
