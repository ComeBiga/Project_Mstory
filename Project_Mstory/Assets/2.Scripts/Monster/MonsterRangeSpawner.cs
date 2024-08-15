using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRangeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnMonster;
    [SerializeField]
    private float _radius = 5f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            for(int i = 0; i < 10; ++i)
                spawn();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        drawCircle(transform.position, _radius, 30);
    }

    private void spawn()
    {
        Vector3 spawnPosition = getRandomPositionInCircle(transform.position, _radius);

        Instantiate(_spawnMonster, spawnPosition, Quaternion.identity);
    }

    private Vector3 getRandomPositionInCircle(Vector3 center, float radius)
    {
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        float randomRadius = Random.Range(0f, radius);

        float x = center.x + randomRadius * Mathf.Cos(randomAngle);
        float y = center.y + randomRadius * Mathf.Sin(randomAngle);

        return new Vector3(x, y, center.z);
    }

    private void drawCircle(Vector3 center, float radius, int segments)
    {
        // 원의 각도를 구합니다.
        float angleStep = 360f / segments;
        Vector3 previousPoint = center + new Vector3(radius, 0, 0); // 원의 초기 점

        // 원의 점을 계산하고 연결합니다.
        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad; // 라디안으로 변환
            Vector3 newPoint = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            Gizmos.DrawLine(previousPoint, newPoint); // 이전 점과 새로운 점을 연결
            previousPoint = newPoint; // 이전 점을 업데이트
        }

        // 마지막 선분을 연결하여 원을 완성합니다.
        Vector3 firstPoint = center + new Vector3(radius, 0, 0);
        Gizmos.DrawLine(previousPoint, firstPoint);
    }
}
