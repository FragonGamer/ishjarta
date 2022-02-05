using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] protected float resistance;
    [SerializeField] protected float currentResistance;
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

    public abstract void ReceiveDamage(int damage);

    public abstract void Attack(Vector2 vector);


    private void Awake()
    {
        statusEffectHandler = ScriptableObject.CreateInstance<StatusEffectHandler>();
    }

    StatusEffectHandler statusEffectHandler;

    public void HandleEffects()
    {
        BaseEffect baseEffect = statusEffectHandler.Frost;
        if (baseEffect != null)
        {
            speedModifier = baseEffect.Effect();
            if (!baseEffect.IsActive)
            {
                statusEffectHandler.RemoveFrost();
                speedModifier = 1;
            }
        }

        baseEffect = statusEffectHandler.Poisining;
        if (baseEffect != null)
        {
            currentHealth = baseEffect.Effect(currentHealth);
            currentHealth = currentHealth <= 0 ? 0 : currentHealth;
            if (!baseEffect.IsActive) statusEffectHandler.RemovePoisining();
        }

        baseEffect = statusEffectHandler.Incineration;
        if (baseEffect != null)
        {
            currentResistance = baseEffect.Effect(resistance, ref currentHealth);
            Debug.Log(baseEffect.DurationRemaining);
            currentHealth = currentHealth <= 0 ? 0 : currentHealth;
            currentResistance = currentResistance <= 0 ? 0 : currentResistance;
            if (!baseEffect.IsActive)
            {
                statusEffectHandler.RemoveIncineration();
                currentResistance = resistance;
            }
        }

        baseEffect = statusEffectHandler.Regeneration;
        if (baseEffect != null)
        {
            currentHealth = baseEffect.Effect(currentHealth);
            currentHealth = currentHealth >= maxHealth ? maxHealth : currentHealth;
            if (!baseEffect.IsActive) statusEffectHandler.RemoveRegeneration();
        }

        baseEffect = statusEffectHandler.Speed;
        if (baseEffect != null)
        {
            Debug.Log(baseEffect.DurationRemaining);
            speedModifier = baseEffect.Effect();
            if (!baseEffect.IsActive)
            {
                statusEffectHandler.RemoveSpeed();
                speedModifier = 1;
            }
        }

        baseEffect = statusEffectHandler.Strength;
        if (baseEffect != null)
        {
            damageModifier = baseEffect.Effect();
            if (!baseEffect.IsActive)
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
    public void AddEffectRange(IEnumerable<BaseEffect> effect)
    {
        if (effect != null)
            statusEffectHandler.AddEffectRange(effect.ToArray());
    }
}
