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
                // 플레이어를 향해 이동
                MoveTowardsPlayer();
            }
            else
            {
                // 플레이어와의 거리 내에서 공격 로직을 여기에 추가할 수 있음
                // Debug.Log("Attack Player!");
                _characterAttack.Attack();
            }
        }
        else
        {
            // 플레이어를 탐지 범위 내에서 찾기 위해 OverlapCollider 호출
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
        // Collider2D 배열을 담을 변수 생성
        var hitColliders = new List<Collider2D>(); // 배열 크기는 필요에 따라 조정

        // CircleCollider2D의 OverlapCollider 메서드를 호출하여 겹치는 콜라이더를 찾음
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
