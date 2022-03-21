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

    //public Enemy(int currentHealth, int maxHealth, int baseHealth, float healthModifier, 
    //    float resistance, float currentResistance, int movementSpeed, float speedModifier, 
    //    int baseDamage, float damageModifier, int attackRate, int range) 
    //    : base(currentHealth, maxHealth, baseHealth, healthModifier, resistance, currentResistance, 
    //        movementSpeed, speedModifier, baseDamage, damageModifier, attackRate, range)
    //{
    //}

    #region getters and setters
    public int GetMovementSpeed()
    {
        return MovementSpeed;
    }
    public float GetRange()
    {
        return Range;
    }
    public int GetSpottingRange()
    {
        return spottingRange;
    }

    public float GetAttackRate()
    {
        return AttackRate;
    }
    public float GetSpeed()
    {
        return MovementSpeed * SpeedModifier;
    }
    //returns Damage which is calculated by baseDamage*damageModifier;
    public float GetDamage()
    {
        return BaseDamage * DamageModifier;
    }
    #endregion


    // Effect which will be passed on to the player
    [field: SerializeField] public BaseEffect EmitEffect { get; set; }

    public override void UpdateHealthBar() { }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        HandleEffects();
    }

    public override void Attack(Vector2 vector)
    {
        throw new System.NotImplementedException();
        
    }
    public override void ReceiveDamage(int damage)
    {
        damage = (damage - ((int)(damage * CurrentResistance)));
        CurrentHealth -= damage;

        Debug.Log(name + " is being attacked");

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
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
