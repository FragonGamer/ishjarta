using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1X1 : Room
{
    public Room1X1(string Name, int RoomId, int RoomSize, int Oriantation) : base(Name, RoomId, RoomSize, Oriantation)
    {
    }

    public override void CalcPossibleDoors(int[,] stage, int maxLength)
    {
        int possibleDorrsLenght = PossibleDoorCount;
        var roomPos = Position;
        for (int i = 0; i < possibleDorrsLenght; i++)
        {
            Tuple<int, int> position;
            switch (i)
            {
                case 0:
                    position = new Tuple<int, int>(roomPos.Item1 - 1, roomPos.Item2);
                    if (CheckBounds(stage,position,maxLength))
                    {
                        var door = Doors.Find(door => door.DoorId == i);
                        if (door == null)
                        {
                            Doors.Add(new Door(position, false, this, i));
                        }
                    }
                    break;
                case 1:
                    position = new Tuple<int, int>(roomPos.Item1, roomPos.Item2 + 1);
                    if (CheckBounds(stage, position, maxLength))
                    {
                        var door = Doors.Find(door => door.DoorId == i);
                        if (door == null)
                        {
                            Doors.Add(new Door(position, false, this, i));
                        }
                    }
                    break;
                case 2:
                    position = new Tuple<int, int>(roomPos.Item1 + 1, roomPos.Item2);
                    if (CheckBounds(stage, position, maxLength))
                    {
                        var door = Doors.Find(door => door.DoorId == i);
                        if (door == null)
                        {
                            Doors.Add(new Door(position, false, this, i));
                        }
                    }
                    break;
                case 3:
                    position = new Tuple<int, int>(roomPos.Item1, roomPos.Item2 - 1);
                    if (CheckBounds(stage, position, maxLength))
                    {
                        var door = Doors.Find(door => door.DoorId == i);
                        if (door == null)
                        {
                            Doors.Add(new Door(position, false, this, i));
                        }
                    }
                    break;
                default:
                    break;
            }


        }
    }

    public override bool CheckPosition(int[,] stage, int midOfLayoutLength)
    {
        bool isValid = true;
        if (Position.Item2 + midOfLayoutLength >= midOfLayoutLength * 2 || Position.Item2 + midOfLayoutLength < 0 ||
            Position.Item1 + midOfLayoutLength >= midOfLayoutLength * 2 || Position.Item1 + midOfLayoutLength < 0 ||
            stage[Position.Item1+ midOfLayoutLength, Position.Item2+ midOfLayoutLength] != 0)
        {
            isValid = false;
        }

        return isValid;
    }

    public override int[,] MarkRoomInLayout(int[,] stage, int midOfLayoutLength)
    {
        stage[Position.Item1 + midOfLayoutLength, Position.Item2 + midOfLayoutLength] = RoomId;
        return stage;
    }
}


