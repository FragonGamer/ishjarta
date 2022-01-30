using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    //Health
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int baseHealth;
    // Is used for enemy scaling and for player itembuff
    [SerializeField] protected float healthModifier;


    //Armor
    protected float resistance;
    protected float currentResistance;
    //Movement
    [SerializeField] protected int movementSpeed;
    [SerializeField] protected float speedModifier;
    //Damage
    [SerializeField] protected int baseDamage;
    // Is used for enemy scaling and for player itembuff
    [SerializeField] protected float damageModifier;
    //AttackRate
    [SerializeField] protected int attackRate;
    //Range
    [SerializeField] protected int range;

    protected abstract void Die();

    public void ReceiveDamage(int damage)
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

    public abstract void Attack(Vector2 vector);



    StatusEffectHandler statusEffectHandler;
    private void Update()
    {
        HandleEffects();
    }

    public void HandleEffects()
    {
        FrostEffect fe = statusEffectHandler.Frost;
        if (fe != null)
        {
            speedModifier = fe.Effect();
            if (!fe.IsActive)
            {
                statusEffectHandler.RemoveFrost();
                speedModifier = 1;
            }
        }

        PoisiningEffect pe = statusEffectHandler.Poisining;
        if (pe != null)
        {
            currentHealth = pe.Effect(currentHealth);
            currentHealth = currentHealth <= 0 ? 0 : currentHealth;
            if (!pe.IsActive) statusEffectHandler.RemovePoisining();
        }

        IncinerationEffect ie = statusEffectHandler.Incineration;
        if (ie != null)
        {
            currentResistance = ie.Effect(resistance, ref currentHealth);
            currentHealth = currentHealth <= 0 ? 0 : currentHealth;
            currentResistance = currentResistance <= 0 ? 0 : currentResistance;
            if (!ie.IsActive)
            {
                statusEffectHandler.RemoveIncineration();
                currentResistance = resistance;
            }
        }

        RegenerationEffect re = statusEffectHandler.Regeneration;
        if (re != null)
        {
            currentHealth = re.Effect(currentHealth);
            currentHealth = currentHealth >= maxHealth ? maxHealth : currentHealth;
            if (!re.IsActive) statusEffectHandler.RemoveRegeneration();
        }

        SpeedEffect spe = statusEffectHandler.Speed;
        if (spe != null)
        {
            speedModifier = spe.Effect();
            if (!spe.IsActive)
            {
                statusEffectHandler.RemoveSpeed();
                speedModifier = 1;
            }
        }

        StrengthEffect ste = statusEffectHandler.Strength;
        if (ste != null)
        {
            damageModifier = ste.Effect();
            if (!ste.IsActive)
            {
                statusEffectHandler.RemoveStrengh();
                damageModifier = 1;
            }
        }
    }

    public void AddEffect(BaseEffect effect)
    {
        if (effect != null)
            statusEffectHandler.AddEffect(effect);
    }
}
