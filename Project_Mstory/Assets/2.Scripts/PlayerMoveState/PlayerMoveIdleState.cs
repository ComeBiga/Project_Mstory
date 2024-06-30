using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveIdleState : PlayerMoveState
{
    public PlayerMoveIdleState(Player player, Animator animator, BoxCollider2D attackCollider) : base(player, animator, attackCollider)
    {
    }

    public override void OnEnterState()
    {
        switch(mPlayer.Direction)
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

    public override void OnExitState()
    {
    }

    public override void OnUpdate(Vector2 direction)
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            mPlayer.ChangeState(mPlayer.MoveRightState);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            mPlayer.ChangeState(mPlayer.MoveLeftState);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            mPlayer.ChangeState(mPlayer.MoveUpState);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            mPlayer.ChangeState(mPlayer.MoveDownState);
        }
    }
}
