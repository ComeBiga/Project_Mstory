using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public bool IsAttacking => mbIsAttacking;

    [SerializeField]
    protected int _attackDamage = 10;
    [SerializeField]
    protected float _attackDelay = .5f;
    [SerializeField]
    protected string _targetTag;
    [SerializeField]
    protected CharacterAnimation _characterAnimation;
    [SerializeField]
    protected CircleCollider2D _attackCircle;
    [SerializeField]
    protected BoxCollider2D _attackBox;

    private bool mbIsAttacking = false;
    private Coroutine mAttackCoroutine = null;

    public virtual void Attack()
    {
        if (!mbIsAttacking)
        {
            mAttackCoroutine = StartCoroutine(eAttack());
        }
    }

    public void AttackLoop()
    {
        if (!mbIsAttacking)
        {
            mAttackCoroutine = StartCoroutine(eAttackLoop());
        }
    }

    public void StopAttack()
    {
        if (mAttackCoroutine == null)
            return;

        mbIsAttacking = false;
        StopCoroutine(mAttackCoroutine);
    }
        
    public void Rotate(int direction)
    {
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private IEnumerator eAttackLoop()
    {
        while(true)
        {
            yield return eAttack();
        }
    }

    private IEnumerator eAttack()
    {
        mbIsAttacking = true;
        _characterAnimation.Attack();

        // Collider2D �迭�� ���� ���� ����
        List<Collider2D> hitColliders = new List<Collider2D>(); // �迭 ũ��� �ʿ信 ���� ����

        var filter = new ContactFilter2D();
        filter.NoFilter();
        // filter.SetLayerMask(_targetLayer);

        // Collider2D�� OverlapCollider �޼��带 ȣ���Ͽ� ��ġ�� �ݶ��̴��� ã��
        int numColliders = _attackBox.OverlapCollider(filter, hitColliders);

        for (int i = 0; i < numColliders; i++)
        {
            if ((hitColliders[i] != null) && hitColliders[i].CompareTag(_targetTag))
            {
                // Debug.Log($"{hitColliders[i].gameObject.name}");

                // ������ �������� �ִ� ����
                hitColliders[i].GetComponentInParent<CharacterHealth>()?.TakeDamage(_attackDamage);
            }
        }

        // ���� �������� ���� �����մϴ�.
        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //foreach (Collider2D enemy in hitEnemies)
        //{
        //    // ������ ���ظ� ������ ����
        //    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        //}

        yield return new WaitForSeconds(_attackDelay); // �ִϸ��̼ǰ� ���� �����̿� ���� ����

        _characterAnimation.Idle();
        mbIsAttacking = false;
    }
}
