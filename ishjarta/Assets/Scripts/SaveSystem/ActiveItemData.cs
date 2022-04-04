using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActiveItemData : ItemData
{
    public float cooldownTime;
    public float activeTime;

    public int activeItemType;

    public ActiveItemData(ActiveItem activeItem) : base(activeItem)
    {
        cooldownTime = activeItem.cooldownTime;
        activeTime = activeItem.activeTime;
        activeItemType = (int)activeItem.ItemType;
    }

    public ActiveItemData(ActiveItem activeItem, Vector2 position) : this(activeItem)
    {
        this.position = position;
    }
}
