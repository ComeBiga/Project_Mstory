using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private Monster monsterPrefab;

    [SerializeField]
    private float width;
    [SerializeField]
    private float height;

    [SerializeField]
    private float interval;
    [SerializeField]
    private float duration;
    [SerializeField]
    private int count;
    [SerializeField]
    private int maxCount;

    private float mTimer;

    private void Start()
    {
        mTimer = 0f;
    }

    private void Update()
    {
        if(mTimer > interval)
        {
            mTimer = 0f;

            Instantiate(monsterPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        }

        mTimer += Time.deltaTime;
    }

    private Vector2 GetRandomSpawnPosition()
    {
        return transform.position;
    }
}
