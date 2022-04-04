using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : EntityData
{
    public InventoryData inventory;
    public PlayerData(Player player)
        : base(player)
    {
        inventory = new InventoryData(player.GetInventory());
    }
}
