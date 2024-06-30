using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveDownState : PlayerMoveState
{
    public PlayerMoveDownState(Player player, Animator animator, BoxCollider2D attackCollider) : base(player, animator, attackCollider)
    {
        mDirectionKey = "Front";
    }

    public override void OnEnterState()
    {
        mPlayer.SetDirection(Player.EDirection.Front);
        // mAnimator.SetTrigger("WalkFront");
        mAnimator.SetTrigger("Front");
    }

    public override void OnExitState()
    {
    }

    public override void OnUpdate(Vector2 direction)
    {
        //if (!mAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Idle") && IsIdle(direction))
        //{
        //    SetIdleAnim();
        //    // mPlayer.ChangeState(mPlayer.MoveIdleState);
        //}

        float vertical = Input.GetAxisRaw("Vertical");
        mAnimator.SetFloat("Vertical", Mathf.Abs(vertical));

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
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    mPlayer.ChangeState(mPlayer.MoveDownState);
        //}

        //if (Input.GetButton("Horizontal") && direction.x > 0.001f)
        //{
        //    mPlayer.ChangeState(mPlayer.MoveRightState);
        //}
        //if (Input.GetButton("Horizontal") && direction.x < -0.001f)
        //{
        //    mPlayer.ChangeState(mPlayer.MoveLeftState);
        //}
        //if (Input.GetButton("Vertical") && direction.y > 0.001f)
        //{
        //    mPlayer.ChangeState(mPlayer.MoveUpState);
        //}
        ////if (Input.GetButton("Vertical") && direction.y < -0.001f)
        ////{
        ////    mPlayer.ChangeState(mPlayer.MoveDownState);
        ////}
    }
}
