using Assets.Scripts.StatusEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string ItemName;
    [SerializeField] protected Sprite Icon;
    protected int dropChanceMax,dropChanceMin;

    public Sprite GetSprite()
    {
        return Icon;
    }

    // Effects which the owner gets
    public List<BaseEffect> OwnerEffects { get; set; }

    // Effects which will be passed on to the enemy
    public List<BaseEffect> EmitEffects { get; set; }
}
