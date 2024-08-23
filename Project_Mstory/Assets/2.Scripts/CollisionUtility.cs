using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollisionUtility
{
    public static void ForeachOverlapCollider(CircleCollider2D collider2D, Action<Transform> onCollide)
    {
        var filter = new ContactFilter2D();
        filter.useTriggers = true;

        var results = new List<Collider2D>();
        int count = collider2D.OverlapCollider(filter, results);

        foreach (Collider2D collider in results)
        {
            onCollide.Invoke(collider.transform);
        }
    }
}
