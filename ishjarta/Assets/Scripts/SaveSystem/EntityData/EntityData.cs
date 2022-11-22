using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the state-storage for Entity
/// </summary>

[System.Serializable]
public abstract class EntityData
{
    public Vector2 position;

    //Health
    public int currentHealth;
    public int maxHealth;
    public int baseHealth;
    // Is used for enemy scaling and for player itembuff
    public float healthModifier;

    //Armor
    public float resistance;
    public float currentResistance;
    //Movement
    public int movementSpeed;
    public float speedModifier;
    //Damage
    public int baseDamage;
    // Is used for enemy scaling and for player itembuff
    public float damageModifier;
    //AttackRate
    public int attackRate;
    //Range
    public float range;

    public EntityData(Entity entity)
    {
        position = entity.transform.position;

        currentHealth = entity.CurrentHealth;
        maxHealth = entity.MaxHealth;
        baseHealth = entity.BaseHealth;
        healthModifier = entity.HealthModifier;

        resistance = entity.Resistance;
        currentResistance = entity.CurrentResistance;

        movementSpeed = entity.MovementSpeed;
        speedModifier = entity.SpeedModifier;

        baseDamage = entity.BaseDamage;
        damageModifier = entity.DamageModifier;

        attackRate = entity.AttackRate;

        range = entity.Range;
    }
}
