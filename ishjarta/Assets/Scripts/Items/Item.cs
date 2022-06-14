using Assets.Scripts.StatusEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Item : ScriptableObject
{
    public string ItemName;
    [SerializeField] public string description;
    [SerializeField] public string fullDescription;

    [field: SerializeField] public Sprite Icon { get; protected set; }
    [field: SerializeField]public Rarity Rarity { get; set; }
    [field: SerializeField]public int Price { get; set; }
    [field: SerializeField]public LevelName[] SpawnLevelPool { get; set; }

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
        }
    }
    #endregion SaveSystem
}
