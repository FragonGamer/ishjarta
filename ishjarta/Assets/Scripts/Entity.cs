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

    public abstract void UpdateHealthBar();


    private void Awake()
    {
        statusEffectHandler = ScriptableObject.CreateInstance<StatusEffectHandler>();
    }
    
    private float timeCounter = 0;
    public void HandleEffects()
    {
        timeCounter += Time.deltaTime;
        if(timeCounter >= 1)
        {
            timeCounter = 0;
            ExecuteEffects();
        }
    }
    StatusEffectHandler statusEffectHandler;
    public void ExecuteEffects()
    {
        float speedBoost = 0, speedDelay = 0;
        BaseEffect[] baseEffects = ConvertEffectsToArray(statusEffectHandler.FrostStat.Frost, statusEffectHandler.FrostStat.PermanentFrost);
        if (baseEffects.Length != 0)
        {
            for(int i = 0; i < baseEffects.Length;i++)
            {
                var baseEffect = baseEffects[i];

                speedDelay += baseEffect.Effect();
                if (!baseEffect.IsActive)
                {
                    statusEffectHandler.RemoveEffect(baseEffect);
                }
            }
        }

        baseEffects = ConvertEffectsToArray(statusEffectHandler.PoisoningStat.Poisoning, statusEffectHandler.PoisoningStat.PermanentPoisoning);
        if (baseEffects.Length != 0)
        {
            for (int i = 0; i < baseEffects.Length; i++)
            {
                var baseEffect = baseEffects[i];

                currentHealth = baseEffect.Effect(currentHealth);
                currentHealth = currentHealth <= 0 ? 0 : currentHealth;
                UpdateHealthBar();
                if (!baseEffect.IsActive) statusEffectHandler.RemoveEffect(baseEffect);
            }
        }

        baseEffects = ConvertEffectsToArray(statusEffectHandler.IncinerationStat.Incineration, statusEffectHandler.IncinerationStat.PermanentIncineration);
        if (baseEffects.Length != 0)
        {
            float decreasedResistance = 0f;
            for (int i = 0; i < baseEffects.Length; i++)
            {
                var baseEffect = baseEffects[i];

                decreasedResistance += baseEffect.Effect(resistance, ref currentHealth);
                currentHealth = currentHealth <= 0 ? 0 : currentHealth;
                UpdateHealthBar();
                if (!baseEffect.IsActive)
                {
                    statusEffectHandler.RemoveEffect(baseEffect);
                    //currentResistance = resistance;
                }
            }
            currentResistance = resistance - decreasedResistance;
            currentResistance = currentResistance <= 0 ? 0 : currentResistance;
        }
        else
        {
            currentResistance = resistance;
        }

        baseEffects = ConvertEffectsToArray(statusEffectHandler.RegenerationStat.Regeneration, statusEffectHandler.RegenerationStat.PermanentRegeneration);
        if (baseEffects.Length != 0)
        {
            for (int i = 0; i < baseEffects.Length; i++)
            {
                var baseEffect = baseEffects[i];

                currentHealth = baseEffect.Effect(currentHealth);
                currentHealth = currentHealth >= maxHealth ? maxHealth : currentHealth;
                UpdateHealthBar();
                if (!baseEffect.IsActive) statusEffectHandler.RemoveEffect(baseEffect);
            }
        }

        baseEffects = ConvertEffectsToArray(statusEffectHandler.SpeedStat.Speed, statusEffectHandler.SpeedStat.PermanentSpeed);
        if (baseEffects.Length != 0)
        {
            for (int i = 0; i < baseEffects.Length; i++)
            {
                var baseEffect = baseEffects[i];

                speedBoost += baseEffect.Effect();
                if (!baseEffect.IsActive)
                {
                    statusEffectHandler.RemoveEffect(baseEffect);
                }
            }
        }

        baseEffects = ConvertEffectsToArray(statusEffectHandler.StrengthStat.Strength, statusEffectHandler.StrengthStat.PermanentStrength);
        if (baseEffects.Length != 0)
        {
            float strengthBoost = 1f;
            for (int i = 0; i < baseEffects.Length; i++)
            {
                var baseEffect = baseEffects[i];

                strengthBoost += baseEffect.Effect();
                if (!baseEffect.IsActive)
                {
                    statusEffectHandler.RemoveEffect(baseEffect);
                }
            }
            damageModifier = strengthBoost;
        }
        else
            damageModifier = 1f;

        HUDManager.instance.UpdateAllSpritesAndText();
        speedModifier = 1 + speedBoost - speedDelay;
        //float speedBoost = 1, speedDelay = 1;
        //BaseEffect baseEffect = statusEffectHandler.Frost;
        //if (baseEffect != null)
        //{
        //    speedDelay = baseEffect.Effect();
        //    if (!baseEffect.IsActive)
        //    {
        //        statusEffectHandler.RemoveFrost();
        //        speedModifier = 1;
        //    }
        //}

        //baseEffect = statusEffectHandler.Poisining;
        //if (baseEffect != null)
        //{
        //    currentHealth = baseEffect.Effect(currentHealth);
        //    currentHealth = currentHealth <= 0 ? 0 : currentHealth;
        //    UpdateHealthBar();
        //    if (!baseEffect.IsActive) statusEffectHandler.RemovePoisining();
        //}

        //baseEffect = statusEffectHandler.Incineration;
        //if (baseEffect != null)
        //{
        //    currentResistance = baseEffect.Effect(resistance, ref currentHealth);
        //    currentHealth = currentHealth <= 0 ? 0 : currentHealth;
        //    UpdateHealthBar();
        //    currentResistance = currentResistance <= 0 ? 0 : currentResistance;
        //    if (!baseEffect.IsActive)
        //    {
        //        statusEffectHandler.RemoveIncineration();
        //        currentResistance = resistance;
        //    }
        //}

        //baseEffect = statusEffectHandler.Regeneration;
        //if (baseEffect != null)
        //{
        //    currentHealth = baseEffect.Effect(currentHealth);
        //    currentHealth = currentHealth >= maxHealth ? maxHealth : currentHealth;
        //    UpdateHealthBar();
        //    if (!baseEffect.IsActive) statusEffectHandler.RemoveRegeneration();
        //}

        //baseEffect = statusEffectHandler.Speed;
        //if (baseEffect != null)
        //{
        //    speedBoost = baseEffect.Effect();
        //    if (!baseEffect.IsActive)
        //    {
        //        statusEffectHandler.RemoveSpeed();
        //        speedModifier = 1;
        //    }
        //}

        //baseEffect = statusEffectHandler.Strength;
        //if (baseEffect != null)
        //{
        //    damageModifier = baseEffect.Effect();
        //    if (!baseEffect.IsActive)
        //    {
        //        statusEffectHandler.RemoveStrengh();
        //        damageModifier = 1;
        //    }
        //}
        //HUDManager.instance.UpdateAllSpritesAndText();
        //speedModifier = speedBoost * speedDelay;
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
    public void RemoveEffect(BaseEffect effect)
    {
        if (effect != null)
            statusEffectHandler.RemoveEffect(effect);
    }

    public void RemoveEffectRange(IEnumerable<BaseEffect> effect)
    {
        if (effect != null)
            statusEffectHandler.RemoveEffectRange(effect.ToArray());
    }

    private BaseEffect[] ConvertEffectsToArray(BaseEffect baseEffect, IEnumerable<BaseEffect> baseEffects)
    {
        var result = new List<BaseEffect>();

        if(baseEffect != null)
            result.Add(baseEffect);

        if (baseEffects != null && baseEffects.Count() > 0)
            result.AddRange(baseEffects);

        return result.ToArray();
    }
}
