using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UsableItemData : ItemData
{
    public int usabelItemType;
    public int amount;
    public int maxAmount;

    public UsableItemData(UsableItem usableItem) : base(usableItem)
    {
        usabelItemType = (int)usableItem.type;
        amount = usableItem.Amount;
        maxAmount = usableItem.MaxAmount;
    }

    public UsableItemData(UsableItem usableItem, Vector2 position) : this(usableItem)
    {
        this.position = position;
    }
}