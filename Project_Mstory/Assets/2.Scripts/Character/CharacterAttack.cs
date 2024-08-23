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

        // Collider2D 배열을 담을 변수 생성
        List<Collider2D> hitColliders = new List<Collider2D>(); // 배열 크기는 필요에 따라 조정

        var filter = new ContactFilter2D();
        filter.NoFilter();
        // filter.SetLayerMask(_targetLayer);

        // Collider2D의 OverlapCollider 메서드를 호출하여 겹치는 콜라이더를 찾음
        int numColliders = _attackBox.OverlapCollider(filter, hitColliders);

        for (int i = 0; i < numColliders; i++)
        {
            if ((hitColliders[i] != null) && hitColliders[i].CompareTag(_targetTag))
            {
                // Debug.Log($"{hitColliders[i].gameObject.name}");

                // 적에게 데미지를 주는 로직
                hitColliders[i].GetComponentInParent<CharacterHealth>()?.TakeDamage(_attackDamage);
            }
        }

        // 공격 지점에서 적을 감지합니다.
        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //foreach (Collider2D enemy in hitEnemies)
        //{
        //    // 적에게 피해를 입히는 로직
        //    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        //}

        yield return new WaitForSeconds(_attackDelay); // 애니메이션과 공격 딜레이에 맞춰 조정

        _characterAnimation.Idle();
        mbIsAttacking = false;
    }
}
