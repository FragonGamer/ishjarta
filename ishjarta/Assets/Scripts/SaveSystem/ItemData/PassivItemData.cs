using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the state-storage for PassivItem
/// </summary>
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
