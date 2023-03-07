using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SaveSystem.Dungeon
{
    [System.Serializable]
    public class DoorData
    {
        public string itemName;


        public bool doorIsOpen;
        public DoorData connectedDoor = null;
        public DirectionData direction;

        //[SerializeField] public Tile closedDoorTile;
        public bool isLocked;
        public bool wasLocked;
        //public Tile lockedDoorTile;
        public float range;
        //private Player player;
        //InputMaster inputMaster;
        public bool isInRange;

        public RoomData room;
        public RoomData connectedDoorRoom;

        public DoorData(Door door)
        {
            itemName = door.name.Replace("(Clone)", "");

            doorIsOpen = door.doorIsOpen;
            if(door != null && door.ConnectedDoor != null && door.ConnectedDoor.GetComponent<Door>() != null && connectedDoor != null)
            {
                var doorComponent = door.ConnectedDoor.GetComponent<Door>();
                connectedDoor = new DoorData(doorComponent);
            }


            direction = Enum.Parse<DirectionData>(door.direction.ToString());

            isLocked = door.isLocked;
            wasLocked = door.wasLocked;
            range = door.range;
            isInRange= door.isInRange;

            if(door.room != null)
            {
                room = new RoomData(door.room, false);
            }
            if(door.ConnectedDoorRoom != null)
            {
                connectedDoorRoom = new RoomData(door.ConnectedDoorRoom, false);
            }
        }
    }

    [System.Serializable]
    public enum DirectionData
    {
        West,
        East,
        North,
        South
    }
}
