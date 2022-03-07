using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int range;

    public EntityData(Entity entity)
    {
        position = entity.transform.position;

        currentHealth = entity.GetCurrentHealth();
        maxHealth = entity.GetMaxHealth();
        baseHealth = entity.GetBaseHealth();
        healthModifier = entity.GetHealthModifier();

        resistance = entity.GetResistance();
        currentResistance = entity.GetCurrentResistance();

        movementSpeed = entity.GetMovementSpeed();
        speedModifier = entity.GetSpeedModifier();

        baseDamage = entity.GetBaseDamage();
        damageModifier = entity.GetDamageModifier();

        attackRate = entity.GetAttackRate();

        range = entity.GetRange();
    }
}
