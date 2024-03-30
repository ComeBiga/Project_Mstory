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
    [SerializeField]
    private float attackInterval = 3f;
    [SerializeField]
    private int attackDamage = 1;

    private Player mTargetPlayer;
    private float mAttackTimer;

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

    private void Start()
    {
        mAttackTimer = 0f;
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
        else
        {
            if(mTargetPlayer != null)
                mTargetPlayer = null;
        }

        if(mTargetPlayer != null)
        {
            var attackColliders = new List<Collider2D>();
            int searchCount_Attack = colliderAttackRange.OverlapCollider(filter, attackColliders);
            bool bAttaking = false;

            if(searchCount_Attack > 0)
            {
                foreach (Collider2D collider in attackColliders)
                {
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        // Debug.Log($"Detected! {collider.gameObject.name}");

                        bAttaking = true;
                    }
                }
            }

            if(bAttaking)
            {
                if(mAttackTimer > attackInterval)
                {
                    mAttackTimer = 0f;

                    mTargetPlayer.TakeDamage(attackDamage);
                }

                mAttackTimer += Time.deltaTime;
            }
            else
            {
                Vector3 direction = mTargetPlayer.transform.position - transform.position;

                transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            }
        }
    }
}
