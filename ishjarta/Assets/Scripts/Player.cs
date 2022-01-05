using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] int rangeModifier;
    [SerializeField] int luck;
    [SerializeField] int maxResistance;
    [SerializeField] int armor;
    public Inventory inventory;

   

    public override void Attack(Vector2 vector)
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), range);

        if (hit)
        {
            if (hit.collider.tag == "Enemy")
                hit.collider.GetComponent<Entity>().ReceiveDamage((int)(this.baseDamage * damageModifier));
        }
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }
}