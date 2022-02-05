using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.AI;

public class Enemy : Entity
{
    [SerializeField] Animator animator;

    [SerializeField] private int spottingRange = 1;
    [field: SerializeField] public int dropRate { get; private set; }
    [field: SerializeField] public bool hasSpottedPlayer { get; set; }
    [field: SerializeField] public bool isInRange { get; set; }
    private float time;
    #region getters and setters
    public int GetMovementSpeed()
    {
        return movementSpeed;
    }
    public int GetRange()
    {
        return range;
    }
    public int GetSpottingRange()
    {
        return spottingRange;
    }

    public float GetAttackRate()
    {
        return attackRate;
    }
    public float GetSpeed()
    {
        return movementSpeed * speedModifier;
    }
    //returns Damage which is calculated by baseDamage*damageModifier;
    public float GetDamage()
    {
        return baseDamage * damageModifier;
    }
    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override void Attack(Vector2 vector)
    {
        throw new System.NotImplementedException();
        
    }
    public override void ReceiveDamage(int damage)
    {
        damage = (damage - ((int)(damage * currentResistance)));
        currentHealth -= damage;

        Debug.Log(name + " is being attacked");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    protected override void Die()
    {
        if (gameObject.GetComponent<CircleCollider2D>() != null)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        if (gameObject.GetComponent<BoxCollider2D>() != null)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        gameObject.GetComponent<AIPath>().canMove = false;
        animator.SetBool("isDead", true);

    }


}
