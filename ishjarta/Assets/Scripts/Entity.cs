using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    //Health
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int baseHealth;
    [SerializeField] protected float healthModifier;
    
    
    //Armor
    protected float resistance;
    //Movement
    [SerializeField] protected int movementSpeed;
    //Damage
    [SerializeField] protected int baseDamage;
    [SerializeField] protected float damageModifier;
    //AttackRate
    [SerializeField] protected int attackRate;
    //Range
    [SerializeField] protected int range;
    
    protected abstract void Die();
    

    public void ReceiveDamage(int damage)
    {
        damage = (damage - ((int)( damage * resistance))) ;
        currentHealth -= damage;

        Debug.Log(name + " is being attacked");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public abstract void Attack(Vector2 vector);
    
    
}
