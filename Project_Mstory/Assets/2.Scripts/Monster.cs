using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private int health;

    public void Damage(int damage)
    {
        health -= damage;

        Debug.Log($"hp : {health}");
    }
}
