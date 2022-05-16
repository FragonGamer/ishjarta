using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : EntityData
{
    public InventoryData inventory;

    public int roomId;

    public PlayerData(Player player)
        : base(player)
    {
        inventory = new InventoryData(player.GetInventory());
        if(player.currentRoom != null)
            roomId = player.currentRoom.RoomId;
        else
            roomId = -1;
    }
}
