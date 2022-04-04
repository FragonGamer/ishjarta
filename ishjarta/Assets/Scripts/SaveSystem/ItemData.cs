using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public Vector2 position;

    public string itemName;
    public string spriteName;
    public int dropChanceMax;
    public int dropChanceMin;

    public List<EffectData> ownerEffects = new List<EffectData>();
    public List<EffectData> emitEffects = new List<EffectData>();

    public ItemData(Item item)
    {
        itemName = item.ItemName;
        spriteName = item.Icon?.name;
        dropChanceMax = item.DropChanceMax;
        dropChanceMin = item.DropChanceMin;

        foreach (var element in item.OwnerEffects)
        {
            if(element != null)
                ownerEffects.Add(new EffectData(element));
        }


        foreach (var element in item.EmitEffects)
        {
            if(element != null)
                emitEffects.Add(new EffectData(element));
        }
    }
}
