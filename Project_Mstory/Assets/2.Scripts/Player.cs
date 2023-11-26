using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private Animator animator;

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

            animator.SetTrigger("Attack");
        }
    }
}
