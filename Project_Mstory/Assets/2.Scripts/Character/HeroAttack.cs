using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : CharacterAttack
{
    [SerializeField]
    private CharacterMovement _characterMovement;
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private CircleCollider2D _detectionCircle;

    private bool mbDetectTarget = false;
    private Transform mTargetTransform = null;

    public override void Attack()
    {
        var filter = new ContactFilter2D();
        filter.useTriggers = true;

        var results = new List<Collider2D>();
        int count = _detectionCircle.OverlapCollider(filter, results);

        mbDetectTarget = false;

        foreach (Collider2D collider in results)
        {
            if (collider.transform.CompareTag(_targetTag))
            {
                mbDetectTarget = true;
                mTargetTransform = collider.transform;

                break;
            }
        }

        if (mbDetectTarget)
        {
            filter = new ContactFilter2D();
            filter.useTriggers = true;

            results = new List<Collider2D>();
            count = _attackCircle.OverlapCollider(filter, results);

            bool bAttack = false;

            foreach (Collider2D collider in results)
            {
                if (collider.transform == mTargetTransform)
                {
                    bAttack = true;

                    break;
                }
            }

            if (bAttack)
            {
                _characterMovement.StopMove();
                base.Attack();
            }
            else
            {
                Vector3 direction = (mTargetTransform.position - transform.position).normalized;

                _characterMovement.Move(direction);
            }
        }
        else
        {
            // _characterMovement.Move(Vector2.zero);
        }
    }
}
