using Assets.Scripts.StatusEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.Serialization;
using System;
[PreferBinarySerialization]
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


}
