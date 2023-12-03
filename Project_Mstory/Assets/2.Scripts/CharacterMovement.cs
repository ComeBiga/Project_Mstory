using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public enum EDirection { Front, Back, Left, Right };

    private EDirection mDirection = EDirection.Front;
    public EDirection Direction => mDirection;

    [SerializeField]
    private float moveSpeed = 10f;

    [Header("Animation")]
    [SerializeField]
    private Animator animator;

    // private Vector3 mDirection = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        var deltaDirection = new Vector3(horizontal, vertical);

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        if (Input.GetButton("Horizontal") && horizontal > 0.001f)
        {
            mDirection = EDirection.Right;
            // mDirection = Vector3.right;
            animator.SetFloat("hDirection", 1f);
            // animator.SetFloat("hDirection", mDirection.x);
            animator.SetFloat("vDirection", 0);
        }
        if (Input.GetButton("Horizontal") && horizontal < -0.001f)
        {
            mDirection = EDirection.Left;
            // mDirection = Vector3.left;
            animator.SetFloat("hDirection", -1f);
            // animator.SetFloat("hDirection", mDirection.x);
            animator.SetFloat("vDirection", 0);
        }
        if (Input.GetButton("Vertical") && vertical > 0.001f)
        {
            mDirection = EDirection.Back;
            // mDirection = Vector3.up;
            animator.SetFloat("hDirection", 0);
            animator.SetFloat("vDirection", 1f);
            // animator.SetFloat("vDirection", mDirection.y);
        }
        if (Input.GetButton("Vertical") && vertical < -0.001f)
        {
            mDirection = EDirection.Front;
            // mDirection = Vector3.down;
            animator.SetFloat("hDirection", 0);
            animator.SetFloat("vDirection", -1f);
            // animator.SetFloat("vDirection", mDirection.y);
        }

        //if (Input.GetButtonUp("Horizontal") && mDirection.x > .001f)
        //{
        //    animator.SetTrigger("Idle_Right");
        //}
        //if (Input.GetButtonUp("Horizontal") && mDirection.x < -.001f)
        //{
        //    animator.SetTrigger("Idle_Left");
        //}
        //if (Input.GetButtonUp("Vertical") && mDirection.y > .001f)
        //{
        //    animator.SetTrigger("Idle_Back");
        //}
        //if (Input.GetButtonUp("Vertical") && mDirection.y < -.001f)
        //{
        //    animator.SetTrigger("Idle_Front");
        //}

        transform.position += moveSpeed * deltaDirection * Time.deltaTime;
    }
}
