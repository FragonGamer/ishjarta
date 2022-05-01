using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    //Health
    [field: SerializeField] private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    [field: SerializeField] public int MaxHealth { get; protected set; }
    [field: SerializeField] public int BaseHealth { get; protected set; }
    // Is used for enemy scaling and for player itembuff
    [field: SerializeField] public float HealthModifier { get; protected set; }

    //Armor
    [field: SerializeField] public float Resistance { get; protected set; }
    [field: SerializeField] public float CurrentResistance { get; protected set; }
    //Movement
    [field: SerializeField] public int MovementSpeed { get; protected set; }
    [field: SerializeField] public float SpeedModifier { get; protected set; }
    //Damage
    [field: SerializeField] public int BaseDamage { get; protected set; }
    // Is used for enemy scaling and for player itembuff
    [field: SerializeField] public float DamageModifier { get; protected set; }
    //AttackRate
    [field: SerializeField] public int AttackRate { get; protected set; }
    //Range
    [field: SerializeField] public float Range { get; protected set; }
    //Width
    [field: SerializeField] public float Width { get; protected set; }

    #region SaveSystem
    private bool isEntityInitialized = false;
    protected void Init(EntityData entityData)
    {
        if(!isEntityInitialized)
        {
            isEntityInitialized = true;

            this.CurrentHealth = entityData.currentHealth;
            this.MaxHealth = entityData.maxHealth;
            this.BaseHealth = entityData.baseHealth;
            this.HealthModifier = entityData.healthModifier;
            this.Resistance = entityData.resistance;
            this.CurrentResistance = entityData.currentResistance;
            this.MovementSpeed = entityData.movementSpeed;
            this.SpeedModifier = entityData.speedModifier;
            this.BaseDamage = entityData.baseDamage;
            this.DamageModifier = entityData.damageModifier;
            this.AttackRate = entityData.attackRate;
            this.Range = entityData.range;
        }
    }
    #endregion SaveSystem

    protected abstract void Die();

    public abstract void ReceiveDamage(int damage);


    public abstract void Attack(Vector2 vector, float attackChargeModifier);

    public abstract void UpdateHealthBar();

    public int DealingDamage
    {
        get
        {
            return (int)(this.BaseDamage * DamageModifier);
        }
    }

    private void Awake()
    {
        statusEffectHandler = ScriptableObject.CreateInstance<StatusEffectHandler>();
    }

    private float timeCounter = 0;
    public void HandleEffects()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= 1)
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
            for (int i = 0; i < baseEffects.Length; i++)
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

                CurrentHealth = baseEffect.Effect(CurrentHealth);
                CurrentHealth = CurrentHealth <= 0 ? 0 : CurrentHealth;
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

                decreasedResistance += baseEffect.Effect(Resistance, ref currentHealth);
                CurrentHealth = CurrentHealth <= 0 ? 0 : CurrentHealth;
                UpdateHealthBar();
                if (!baseEffect.IsActive)
                {
                    statusEffectHandler.RemoveEffect(baseEffect);
                    //currentResistance = resistance;
                }
            }
            CurrentResistance = Resistance - decreasedResistance;
            CurrentResistance = CurrentResistance <= 0 ? 0 : CurrentResistance;
        }
        else
        {
            CurrentResistance = Resistance;
        }

        baseEffects = ConvertEffectsToArray(statusEffectHandler.RegenerationStat.Regeneration, statusEffectHandler.RegenerationStat.PermanentRegeneration);
        if (baseEffects.Length != 0)
        {
            for (int i = 0; i < baseEffects.Length; i++)
            {
                var baseEffect = baseEffects[i];

                CurrentHealth = baseEffect.Effect(CurrentHealth);
                CurrentHealth = CurrentHealth >= MaxHealth ? MaxHealth : CurrentHealth;
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
            DamageModifier = strengthBoost;
        }
        else
            DamageModifier = 1f;

        HUDManager.instance.UpdateAllSpritesAndText();
        SpeedModifier = 1 + speedBoost - speedDelay;
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

        if (baseEffect != null)
            result.Add(baseEffect);

        if (baseEffects != null && baseEffects.Count() > 0)
            result.AddRange(baseEffects);

        return result.ToArray();
    }

    public static Vector2 RotateVector2(Vector2 vec, float angle)
    {
        Vector2 result = new Vector2();

        result.x = (float)(Math.Cos(angle) * vec.x - Math.Sin(angle) * vec.y);
        result.y = (float)(Math.Sin(angle) * vec.x + Math.Cos(angle) * vec.y);

        return result;
    }
}
