using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the state-storage for ActiveItem
/// </summary>
[System.Serializable]
public class ActiveItemData : ItemData
{
    public int activeItemType;

    public ActiveItemData(ActiveItem activeItem) : base(activeItem)
    {
        activeItemType = (int)activeItem.ItemType;
    }

    public ActiveItemData(ActiveItem activeItem, Vector2 position) : this(activeItem)
    {
        this.position = position;
    }
}
