using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum EDirection { Front, Back, Left, Right };

    private EDirection mDirection = EDirection.Front;
    public EDirection Direction => mDirection;

    [SerializeField]
    private BoxCollider2D boxCollider;

    [Header("AttackBox")]
    [SerializeField]
    private BoxCollider2D boxCollider_FrontAttack;
    [SerializeField]
    private BoxCollider2D boxCollider_BackAttack;
    [SerializeField]
    private BoxCollider2D boxCollider_LeftAttack;
    [SerializeField]
    private BoxCollider2D boxCollider_RightAttack;

    [Header("Animation")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float moveSpeed = 10f;

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            var filter = new ContactFilter2D();
            filter.useTriggers = true;

            var results = new List<Collider2D>();
            int count = boxCollider.OverlapCollider(filter, results);

            foreach (Collider2D collider in results)
            {
                if (collider.tag == "NPC")
                {
                    Debug.Log($"{collider.gameObject.name}");
                    break;
                }
            }

            UpdateAttack();
            //count = boxCollider_FrontAttack.OverlapCollider(filter, results);

            //foreach (Collider2D collider in results)
            //{
            //    if (collider.tag == "Monster")
            //    {
            //        Debug.Log($"{collider.gameObject.name}");
            //        break;
            //    }
            //}

            animator.SetTrigger("Attack");
        }

        UpdateMovement();
    }

    private void UpdateMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        var deltaDirection = new Vector3(horizontal, vertical);

        transform.position += moveSpeed * deltaDirection * Time.deltaTime;

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        if (Input.GetButton("Horizontal") && horizontal > 0.001f)
        {
            mDirection = EDirection.Right;
            animator.SetFloat("hDirection", 1f);
            animator.SetFloat("vDirection", 0);
        }
        if (Input.GetButton("Horizontal") && horizontal < -0.001f)
        {
            mDirection = EDirection.Left;
            animator.SetFloat("hDirection", -1f);
            animator.SetFloat("vDirection", 0);
        }
        if (Input.GetButton("Vertical") && vertical > 0.001f)
        {
            mDirection = EDirection.Back;
            animator.SetFloat("hDirection", 0);
            animator.SetFloat("vDirection", 1f);
        }
        if (Input.GetButton("Vertical") && vertical < -0.001f)
        {
            mDirection = EDirection.Front;
            animator.SetFloat("hDirection", 0);
            animator.SetFloat("vDirection", -1f);
        }
    }

    private void UpdateAttack()
    {
        BoxCollider2D boxCollider = null;

        switch(mDirection)
        {
            case EDirection.Front:
                boxCollider = boxCollider_FrontAttack;
                break;
            case EDirection.Back:
                boxCollider = boxCollider_BackAttack;
                break;
            case EDirection.Left:
                boxCollider = boxCollider_LeftAttack;
                break;
            case EDirection.Right:
                boxCollider = boxCollider_RightAttack;
                break;
            default:
#if UNITY_EDITOR
                Debug.LogError($"Wrong direction enum type![type:{mDirection}]");
#endif
                break;
        }

        var filter = new ContactFilter2D();
        filter.useTriggers = true;

        var results = new List<Collider2D>();
        int count = boxCollider.OverlapCollider(filter, results);

        foreach (Collider2D collider in results)
        {
            if (collider.tag == "Monster")
            {
                collider.gameObject.GetComponent<Monster>().Damage(1);
                Debug.Log($"{mDirection} Attack to '{collider.gameObject.name}'");
                break;
            }
        }
    }
}
