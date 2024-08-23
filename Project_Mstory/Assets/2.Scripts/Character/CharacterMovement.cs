using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool IsMoving => mMovement.magnitude > 0.001f;

    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private CharacterAttack _characterAttack;
    [SerializeField]
    private CharacterAnimation _characterAnimation;
    [SerializeField]
    private Animator _animator;

    private Vector2 mMovement;

    public void Move(Vector2 direction)
    {
        mMovement = direction;

        if (IsMoving)
        {
            _characterAnimation.Walk();

            if (mMovement.x > .001f)
            {
                _characterAnimation.Rotate(-1);
                _characterAttack.Rotate(-1);
            }
            else
            {
                _characterAnimation.Rotate(1);
                _characterAttack.Rotate(1);
            }
        }
        else
        {
            _characterAnimation.Idle();
        }
    }

    public void StopMove()
    {
        mMovement = Vector2.zero;
    }

    private void Start()
    {
        if(_rigidbody2D == null)
            _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_characterAttack.IsAttacking)
        {
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }

        _rigidbody2D.velocity = mMovement * _moveSpeed;
    }
}
