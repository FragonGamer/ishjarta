using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] int dropRate, spottingRange;
    [SerializeField] bool hasSpottedPlayer, isInRange;
    public override void Attack(Vector2 vector)
    {
        throw new System.NotImplementedException();
    }

    protected override void Die()
    {
        GameObject.Destroy(gameObject);
    }
}
