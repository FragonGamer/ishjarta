using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] int rangeModifier;
    [SerializeField] int luck;
    //[SerializeField] int maxResistance;

    public void CalcResistence()
    {

            int armorAmount = inventory.GetArmor().Amount;
            resistance = (1 * armorAmount) / (2.5f + armorAmount) * 0.25f;
        
    }

    public float GetResistence()
    {
        return resistance;
    }
    
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