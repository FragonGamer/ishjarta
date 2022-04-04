using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PassivItemData : ItemData
{
    public int passivItemType;

    public PassivItemData(PassiveItem passivItem) : base(passivItem)
    {
        passivItemType = (int)passivItem.ItemType;
    }

    public PassivItemData(PassiveItem passivItem, Vector2 position) : this(passivItem)
    {
        this.position = position;
    }
}
