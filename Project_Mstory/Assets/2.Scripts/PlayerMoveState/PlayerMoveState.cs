using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMoveState
{
    protected Player mPlayer;
    protected Animator mAnimator;
    protected BoxCollider2D mAttackCollider;
    protected string mDirectionKey;

    public PlayerMoveState(Player player, Animator animator, BoxCollider2D attackCollider)
    {
        mPlayer = player;
        mAnimator = animator;
        mAttackCollider = attackCollider;
    }

    public abstract void OnEnterState();
    public abstract void OnExitState();
    public abstract void OnUpdate(Vector2 direction);

    public void Attack()
    {
        var filter = new ContactFilter2D();
        filter.useTriggers = true;

        var results = new List<Collider2D>();
        int count = mAttackCollider.OverlapCollider(filter, results);

        foreach (Collider2D collider in results)
        {
            if (collider.tag == "Monster")
            {
                Monster targetMonster = collider.gameObject.GetComponent<Monster>();
                targetMonster.Damage(mPlayer.AttackPower,
                                    onDied: () =>
                                    {
                                        mPlayer.UpdateQuest();
                                    });

                // Debug.Log($"{mDirection} Attack to '{collider.gameObject.name}'");
                break;
            }
        }

        mAnimator.SetTrigger($"Attack{mDirectionKey}");
    }

    protected bool IsIdle(Vector2 direction)
    {
        return direction.x < .001f && direction.x > -.001f && direction.y < .001f && direction.y > -.001f;
    }

    protected void SetIdleAnim()
    {
        switch (mPlayer.Direction)
        {
            case Player.EDirection.Front:
                mAnimator.SetTrigger("IdleFront");
                break;
            case Player.EDirection.Back:
                mAnimator.SetTrigger("IdleBack");
                break;
            case Player.EDirection.Left:
                mAnimator.SetTrigger("IdleLeft");
                break;
            case Player.EDirection.Right:
                mAnimator.SetTrigger("IdleRight");
                break;
        }
    }
}
