using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public bool IsAttacking => mbIsAttacking;

    [SerializeField]
    private int _attackDamage = 10;
    [SerializeField]
    private float _attackDelay = .5f;
    [SerializeField]
    private string _targetTag;
    [SerializeField]
    private CharacterAnimation _characterAnimation;
    [SerializeField]
    private BoxCollider2D _attackBox;

    private bool mbIsAttacking = false;

    public void Attack()
    {
        if (!mbIsAttacking)
        {
            StartCoroutine(eAttack());
        }
    }

    public void Rotate(int direction)
    {
        transform.localScale = new Vector3(direction, 1, 1);
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
