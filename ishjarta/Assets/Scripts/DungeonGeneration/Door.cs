using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door
{
    public Tuple<int,int> Position { get; set; }
    public bool inUse { get; set; } = false;
    public Room Room { get; set; }
    public Door AttatchedDoor { get; set; }
    public int DoorId { get; set; }

    public Door(Tuple<int, int> Position, bool inUse, Room Room, int DoorId)
    {
        this.Position = Position;
        this.inUse = inUse;
        this.Room = Room;
        this.DoorId = DoorId;

    }
}
