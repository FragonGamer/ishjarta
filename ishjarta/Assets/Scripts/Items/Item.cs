using Assets.Scripts.StatusEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Item : ScriptableObject
{
    public string ItemName;
    [field: SerializeField] public Sprite Icon { get; protected set; }
    public int DropChanceMax { get; protected set; }
    public int DropChanceMin { get; protected set; }

    public Sprite GetSprite()
    {
        return Icon;
    }

    // Effects which the owner gets
    [field: SerializeField] public List<BaseEffect> OwnerEffects { get; set; } = new List<BaseEffect>();

    // Effects which will be passed on to the enemy
    [field: SerializeField] public List<BaseEffect> EmitEffects { get; set; } = new List<BaseEffect>();

    #region SaveSystem
    private bool isItemInitialized = false;
    protected void Init(ItemData itemData)
    {
        if (!isItemInitialized)
        {
            isItemInitialized = true;

            ItemName = itemData.itemName;
            DropChanceMax = itemData.dropChanceMax;
            DropChanceMin = itemData.dropChanceMin;

            OwnerEffects = itemData.ownerEffects.Select(x =>
            {
                BaseEffect effect = null;
                if(x.effectType == EffectType.FrostEffect)
                {
                    effect = x.isPermanent ? 
                    FrostEffect.CreateInstance( x.isPermanent, x.powerModifier)
                    :
                    FrostEffect.CreateInstance(x.duration, x.powerModifier);
                }
                else if (x.effectType == EffectType.PoisoningEffect)
                {
                    effect = x.isPermanent ?
                    PoisoningEffect.CreateInstance(x.isPermanent, x.healthChange)
                    :
                    PoisoningEffect.CreateInstance(x.duration, x.healthChange);
                }
                else if (x.effectType == EffectType.IncinerationEffect)
                {
                    effect = x.isPermanent ?
                    IncinerationEffect.CreateInstance(x.isPermanent, x.powerModifier, x.healthChange)
                    :
                    IncinerationEffect.CreateInstance(x.duration, x.powerModifier, x.healthChange);
                }
                else if (x.effectType == EffectType.RegenerationEffect)
                {
                    effect = x.isPermanent ?
                    RegenerationEffect.CreateInstance(x.isPermanent, x.healthChange)
                    :
                    RegenerationEffect.CreateInstance(x.duration, x.healthChange);
                }
                else if (x.effectType == EffectType.SpeedEffect)
                {
                    effect = x.isPermanent ?
                    SpeedEffect.CreateInstance(x.isPermanent, x.powerModifier)
                    :
                    SpeedEffect.CreateInstance(x.duration, x.powerModifier);
                }
                else if (x.effectType == EffectType.StrengthEffect)
                {
                    effect = x.isPermanent ?
                    StrengthEffect.CreateInstance(x.isPermanent, x.powerModifier)
                    :
                    StrengthEffect.CreateInstance(x.duration, x.powerModifier);
                }
                return effect;
            }).ToList();

            EmitEffects = itemData.emitEffects.Select(x =>
            {
                BaseEffect effect = null;
                if (x.effectType == EffectType.FrostEffect)
                {
                    effect = x.isPermanent ?
                    FrostEffect.CreateInstance(x.isPermanent, x.powerModifier)
                    :
                    FrostEffect.CreateInstance(x.duration, x.powerModifier);
                }
                else if (x.effectType == EffectType.PoisoningEffect)
                {
                    effect = x.isPermanent ?
                    PoisoningEffect.CreateInstance(x.isPermanent, x.healthChange)
                    :
                    PoisoningEffect.CreateInstance(x.duration, x.healthChange);
                }
                else if (x.effectType == EffectType.IncinerationEffect)
                {
                    effect = x.isPermanent ?
                    IncinerationEffect.CreateInstance(x.isPermanent, x.powerModifier, x.healthChange)
                    :
                    IncinerationEffect.CreateInstance(x.duration, x.powerModifier, x.healthChange);
                }
                else if (x.effectType == EffectType.RegenerationEffect)
                {
                    effect = x.isPermanent ?
                    RegenerationEffect.CreateInstance(x.isPermanent, x.healthChange)
                    :
                    RegenerationEffect.CreateInstance(x.duration, x.healthChange);
                }
                else if (x.effectType == EffectType.SpeedEffect)
                {
                    effect = x.isPermanent ?
                    SpeedEffect.CreateInstance(x.isPermanent, x.powerModifier)
                    :
                    SpeedEffect.CreateInstance(x.duration, x.powerModifier);
                }
                else if (x.effectType == EffectType.StrengthEffect)
                {
                    effect = x.isPermanent ?
                    StrengthEffect.CreateInstance(x.isPermanent, x.powerModifier)
                    :
                    StrengthEffect.CreateInstance(x.duration, x.powerModifier);
                }
                return effect;
            }).ToList();
        }
    }
    #endregion SaveSystem
}
