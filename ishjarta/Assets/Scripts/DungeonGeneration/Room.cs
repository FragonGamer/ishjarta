using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Room
    
{
    public string Name { get; set; }
    public int RoomId { get; set; }
    public Tuple<int, int> Position { get; set; }
    public int RoomSize { get; set; }
    public int Oriantation { get; set; }
    public List<Door> Doors { get; set; } = new List<Door>();
    public int PossibleDoorCount { 
        get
        {
            switch (RoomSize)
            {
                case 1:
                    return 4;
                    break;
                case 2:
                    return 6;
                    break;
                case 3:
                    return 6;
                    break;
                case 4:
                    return 8;
                    break;
                default:
                    return -1;
                    break;
            }
        }
        }

    public Room(string Name,int RoomId, int RoomSize, int Oriantation)
    {
        this.Name = Name;
        this.RoomId = RoomId;
        this.RoomSize = RoomSize;
        this.Oriantation = Oriantation;
    }
    public abstract void CalcPossibleDoors(int[,] stage, int maxLength);
    public abstract bool CheckPosition(int[,] stage, int midOfLayoutLength);
    public abstract int[,] MarkRoomInLayout(int[,] stage, int midOfLayoutLength);
    public bool CheckBounds(int[,] stage, Tuple<int,int> position, int maxLength) {
        int midlength = maxLength / 2;
        if (position.Item1 + midlength >= 0 && position.Item1 + midlength < maxLength && position.Item2 + midlength >= 0 && position.Item2 + midlength < maxLength)
        {
            return true;
        }
        return false;
    }
    
}
