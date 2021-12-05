using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] int dropRate;

    public override void Attack(Vector2 vector)
    {
        throw new System.NotImplementedException();
    }

    protected override void Die()
    {
        Debug.Log(name + " has died");
        //throw new System.NotImplementedException();
    }
}
