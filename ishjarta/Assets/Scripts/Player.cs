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

    public HealthBar hpBar;
    //void Start()
    //{
    //    hpBar.SetMaxHealth(maxHealth);
    //}

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

    public int DealingDamage
    {
        get
        {
            return (int)(this.baseDamage * damageModifier);
        }
    }



    public override void Attack(Vector2 vector)
    {
        if (GetComponent<PolygonCollider2D>() == null)
        {
            MeleeWeapon w = new();
            w.Width = 0.1f;
            w.Range = 1.2f;
            inventory.CurrentWeapon = w;

            if (inventory.CurrentWeapon is MeleeWeapon melWeapon)
            {
                Vector2 lookdir = (Vector2)Camera.main.ScreenToWorldPoint(vector) - GetComponent<Rigidbody2D>().position;
                float angle = Mathf.Atan2(lookdir.y, lookdir.x);
                if (angle < 0)
                    angle = (float)(Math.PI - Math.Abs(angle) + Math.PI);

                Vector2[] v = new Vector2[]
                {
                    RotateVector2(new Vector2 {x = 0, y = 0}, angle),
                    RotateVector2(new Vector2 {x = melWeapon.Range*(0.7f), y = melWeapon.Width}, angle) ,
                    RotateVector2(new Vector2 {x = melWeapon.Range, y = 0}, angle) ,
                    RotateVector2(new Vector2 {x = melWeapon.Range*(0.7f), y =  melWeapon.Width*(-1)}, angle)
                };

                PolygonCollider2D pc = this.gameObject.AddComponent<PolygonCollider2D>();
                pc.isTrigger = true;
                pc.points = v;

                Destroy(pc, 0.2f);
            }
            else if (inventory.CurrentWeapon is RangedWeapon curWeapon)
            {

            }
        }

    }

    private Vector2 RotateVector2(Vector2 vec, float angle)
    {
        Vector2 result = new Vector2();

        result.x = (float)(Math.Cos(angle) * vec.x - Math.Sin(angle) * vec.y);
        result.y = (float)(Math.Sin(angle) * vec.x + Math.Cos(angle) * vec.y);

        return result;
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }

    new public void ReceiveDamage(int damage)
    {
        damage = (damage - ((int)(damage * resistance)));
        currentHealth -= damage;

        Debug.Log(name + " is being attacked");

        hpBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
}