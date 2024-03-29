using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public event Action<Monster> onDied = null;

    [SerializeField]
    private int health;
    [SerializeField]
    private int exp;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private CircleCollider2D colliderSearchRange;
    [SerializeField]
    private CircleCollider2D colliderAttackRange;
    [SerializeField]
    private float moveSpeed = 5f;

    private Player mTargetPlayer;

    public void Damage(int damage, Action onDied = null)
    {
        health -= damage;

        anim.SetTrigger("Damaged");

        Debug.Log($"hp : {health}");

        if (health <= 0)
        {
            onDied?.Invoke();
            Die();
        }
    }

    public void Die()
    {
        onDied?.Invoke(this);

        Destroy(gameObject);
    }

    private void Update()
    {
        var filter = new ContactFilter2D();
        // filter.SetLayerMask(LayerMask.NameToLayer("Player"));
        // filter.useTriggers = true;
        filter.NoFilter();

        var colliders = new List<Collider2D>();
        int searchCount = colliderSearchRange.OverlapCollider(filter, colliders);

        if (searchCount > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Player"))
                {
                    // Debug.Log($"Detected! {collider.gameObject.name}");

                    mTargetPlayer = collider.transform.GetComponent<Player>();
                }
            }
        }

        if(mTargetPlayer != null)
        {
            Vector3 direction = mTargetPlayer.transform.position - transform.position;

            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }
    }
}
