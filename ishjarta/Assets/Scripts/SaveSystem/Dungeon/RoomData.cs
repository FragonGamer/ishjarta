using Assets.Scripts.SaveSystem.Dungeon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomData
{
    public Vector2 position;

    public string itemName;

    public bool hasVisited;
    public int maxOfThisRoom;
    public bool isCleared;
    public bool isEntered;
    public bool isClosed;
    public int roomId;
    public int distanceToStart = 0;
    //public List<Enemy> Enemies = new List<Enemy>();
    public List<DoorData> doors { get; private set; } = new List<DoorData>();

    public int lenX;
    public int lenY;

    public RoomData(Room room, bool includeDoors = true)
    {
        itemName = room.name.Replace("(Clone)", "");

        hasVisited = room.hasVisited;
        maxOfThisRoom = room.maxOfThisRoom;
        isCleared = room.IsCleared;
        isEntered = room.isEntered;
        isClosed = room.isClosed;
        roomId = room.RoomId;
        distanceToStart = room.DistanceToStart;

        if(includeDoors)
        {
            room.doors.ForEach((door) =>
            {
                var d = door.GetComponent<Door>();
                if (d != null)
                {
                    doors.Add(new DoorData(d));
                }
            });
        }

        lenX = room.lenX;
        lenY = room.lenY;
    }

    public RoomData(Room room, Vector2 position, bool includeDoors = true) : this(room, includeDoors)
    {
        this.position = position;
    }
}
