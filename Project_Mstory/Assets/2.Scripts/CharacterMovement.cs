using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    [Header("Animation")]
    [SerializeField]
    private Animator animator;

    private Vector3 mDirection = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        var direction = new Vector3(horizontal, vertical);

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        if (Input.GetButtonDown("Horizontal") && horizontal > 0.001f)
        {
            
            //mDirection = Vector3.right;
        }
        if (Input.GetButtonDown("Horizontal") && horizontal < -0.001f)
        {
            
            //mDirection = Vector3.left;
        }
        if (Input.GetButtonDown("Vertical") && vertical > 0.001f)
        {
            
            //mDirection = Vector3.up;
        }
        if (Input.GetButtonDown("Vertical") && vertical < -0.001f)
        {
            
            //mDirection = Vector3.down;
        }

        transform.position += moveSpeed * direction * Time.deltaTime;

    }
}
