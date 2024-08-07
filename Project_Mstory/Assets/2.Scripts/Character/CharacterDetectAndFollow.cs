using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetectAndFollow : MonoBehaviour
{
    [SerializeField]
    private CharacterMovement _characterMovement;
    [SerializeField]
    private CharacterAttack _characterAttack;
    [SerializeField]
    private CircleCollider2D _detectCircle;
    [SerializeField]
    private float _detectRadius = 4f;
    [SerializeField]
    private float _attackRadius = 2f;

    private bool mIsPlayerDetected = false;
    private Transform mTrDetectedCharacter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mIsPlayerDetected)
        {
            // MoveTowardsPlayer();
            float distanceToPlayer = Vector2.Distance(transform.position, mTrDetectedCharacter.position);

            if (distanceToPlayer > _attackRadius)
            {
                // �÷��̾ ���� �̵�
                MoveTowardsPlayer();
            }
            else
            {
                // �÷��̾���� �Ÿ� ������ ���� ������ ���⿡ �߰��� �� ����
                // Debug.Log("Attack Player!");
                _characterAttack.Attack();
            }
        }
        else
        {
            // �÷��̾ Ž�� ���� ������ ã�� ���� OverlapCollider ȣ��
            DetectPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (mTrDetectedCharacter.position - transform.position).normalized;
        // transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        _characterMovement.Move(direction);
    }

    private void DetectPlayer()
    {
        // Collider2D �迭�� ���� ���� ����
        var hitColliders = new List<Collider2D>(); // �迭 ũ��� �ʿ信 ���� ����

        // CircleCollider2D�� OverlapCollider �޼��带 ȣ���Ͽ� ��ġ�� �ݶ��̴��� ã��
        int numColliders = _detectCircle.OverlapCollider(new ContactFilter2D().NoFilter(), hitColliders);

        for (int i = 0; i < numColliders; i++)
        {
            if (hitColliders[i] != null && hitColliders[i].CompareTag("Player"))
            {
                mIsPlayerDetected = true;
                mTrDetectedCharacter = hitColliders[i].GetComponentInParent<Character>().transform;
                break;
            }
        }
    }
}
