using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the state-storage for Item
/// </summary>
[System.Serializable]
public class ItemData
{
    public Vector2 position;

    public string itemName;

    public ItemData(Item item)
    {
        itemName = item.ItemName;
    }
}
