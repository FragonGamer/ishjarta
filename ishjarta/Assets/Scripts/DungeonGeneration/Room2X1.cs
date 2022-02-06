using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2X1 : Room
{
    public Room2X1(string Name, int RoomId, int RoomSize, int Oriantation) : base(Name, RoomId, RoomSize, Oriantation)
    {
    }

    public override void CalcPossibleDoors(int[,] stage, int maxLength)
    {
        int possibleDorrsLenght = PossibleDoorCount;
        var roomPos = Position;
        int oriantationMod = Oriantation;
        if (Oriantation == 1)
        {
            oriantationMod = 1;
        }
        else
        {
            oriantationMod = -1;
        }
        for (int i = 0; i < possibleDorrsLenght; i++)
        {
            Tuple<int, int> position;
            switch (i)
            {
                case 0:
                    position = new Tuple<int, int>(roomPos.Item1 - 1 * oriantationMod, roomPos.Item2);
                    if (CheckBounds(stage, position, maxLength))
                    {
                        var door = Doors.Find(door => door.DoorId == i);
                        if (door == null)
                        {
                            Doors.Add(new Door(position, false, this, i));
                        }
                    }
                    break;
                case 1:
                    position = new Tuple<int, int>(roomPos.Item1, roomPos.Item2 + 1 * oriantationMod);
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
                    position = new Tuple<int, int>(roomPos.Item1 + 1 * oriantationMod, roomPos.Item2 + 1 * oriantationMod);
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
                    position = new Tuple<int, int>(roomPos.Item1 + 2 * oriantationMod, roomPos.Item2);
                    if (CheckBounds(stage, position, maxLength))
                    {
                        var door = Doors.Find(door => door.DoorId == i);
                        if (door == null)
                        {
                            Doors.Add(new Door(position, false, this, i));
                        }
                    }
                    break;
                case 4:
                    position = new Tuple<int, int>(roomPos.Item1 + 1 * oriantationMod, roomPos.Item2 - 1 * oriantationMod);
                    if (CheckBounds(stage, position, maxLength))
                    {
                        var door = Doors.Find(door => door.DoorId == i);
                        if (door == null)
                        {
                            Doors.Add(new Door(position, false, this, i));
                        }
                    }
                    break;
                case 5:
                    position = new Tuple<int, int>(roomPos.Item1, roomPos.Item2 - 1 * oriantationMod);
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
        CalcPossibleDoors(stage,midOfLayoutLength*2+1);
        bool isValid = true;
        int oriantationMod = 0;
        switch (Oriantation)
        {
            case 1:
                oriantationMod = 1;
                break;
            case 2:
                oriantationMod = -1;
                break;
            default:
                break;
        }
        int xPos = Position.Item1 + midOfLayoutLength;
        int yPos = Position.Item2 + midOfLayoutLength;
        int totalLength = midOfLayoutLength * 2 + 1;

        bool startPos = xPos >= totalLength || xPos <= 0 ||
            yPos >= totalLength || yPos <= 0 ||
            stage[xPos,yPos] != 0;

        xPos += 1 * oriantationMod;

        bool secondPos = xPos >= totalLength || xPos <= 0 ||
            yPos >= totalLength || yPos <= 0 ||
            stage[xPos, yPos] != 0;

        if (startPos || secondPos)
        {
            isValid = false;
        }
        return isValid;
    }

    public override int[,] MarkRoomInLayout(int[,] stage, int midOfLayoutLength)
    {
        int oriantationMod = 0;
        switch (Oriantation)
        {
            case 1:
                oriantationMod = 1;
                break;
            case 2:
                oriantationMod = -1;
                break;
            default:
                break;
        }
        stage[Position.Item1 + midOfLayoutLength, Position.Item2 + midOfLayoutLength] = RoomId;
        stage[Position.Item1 + midOfLayoutLength + 1 * oriantationMod, Position.Item2 + midOfLayoutLength] = RoomId;
        return stage;
    }
}

