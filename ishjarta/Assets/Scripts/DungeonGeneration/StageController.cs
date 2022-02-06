using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageController : MonoBehaviour
{
    const int layoutLength = 11;
    int midOfLayoutLength = Convert.ToInt32(layoutLength / 2);
    private int lastRoomNumber = 1;
    const int maxRoomNumbers = 18;
    public int[,] worldLayout = new int[11, 11];
    public List<Room> rooms = new List<Room>();

    public void PrintStageLayout()
    {
        string world = "";
        for (int i = 0; i < worldLayout.GetLength(0); i++)
        {
            for (int j = 0; j < worldLayout.GetLength(1); j++)
            {
                world += worldLayout[i, j] + "   ";
            }
            world += "\n\n";
        }
        Debug.Log(world);
    }
    public void CreateStage()
    {

        CreateWorldLayout();
        PrintStageLayout();


    }
    public void AddRoomToLayout()
    {
        lastRoomNumber++;
        System.Random random = new System.Random();
        Room newRoom = null;
        Room parentRoom = null;
        int parentRoomDoorId = 0;

        //Room Size / Oriantation START
        int roomSize = random.Next(1, 5);
        int orientation = random.Next(1, 3);

        switch (roomSize)
        {


            case 1: newRoom = new Room1X1("StandardRoom", lastRoomNumber, roomSize, orientation); break;
            case 2:
                newRoom = new Room1X2("StandardRoom", lastRoomNumber, roomSize, orientation);
                break;
            case 3:
                newRoom = new Room2X1("StandardRoom", lastRoomNumber, roomSize, orientation);

                break;
            case 4: newRoom = new Room2X2("StandardRoom", lastRoomNumber, roomSize, orientation); break;
            default:
                return;
                break;

        }
        //Room Size / Oriantation END
        bool parentRoomFailure = false;
        int parentRoomFailureCounter = 0;
        while (!parentRoomFailure)
        {
            // Parent room
            int parentRoomId = random.Next(1, lastRoomNumber);
            parentRoom = rooms.FirstOrDefault(room => room.RoomId == parentRoomId);
            parentRoom.CalcPossibleDoors(worldLayout,layoutLength);
            // Parent room


            parentRoomDoorId = 0;
            int positionFailureCounter = 0;

            Tuple<int, int> position = new Tuple<int, int>(0, 0);
            bool posIsvalid = false;

            while (!posIsvalid && positionFailureCounter < 20)
            {
              
                parentRoomDoorId = random.Next(1, parentRoom.PossibleDoorCount+1);
                try
                {
                    newRoom.Position = parentRoom.Doors.Find(door => door.DoorId == parentRoomDoorId-1).Position;
                }
                catch (NullReferenceException e)
                {
                    Debug.Log("THIS");

                    throw e;
                }
                if (newRoom.CheckPosition(worldLayout, midOfLayoutLength))
                {
                    posIsvalid = true;
                }
                else
                {
                    positionFailureCounter++;
                }


            }
            if (posIsvalid)
            {
                break;
            }
            if (++parentRoomFailureCounter >= 10)
            {
                parentRoomFailure = true;
            }
        }
        if (parentRoomFailure == true)
        {
            lastRoomNumber--;
            return;
        }

        
        worldLayout = newRoom.MarkRoomInLayout(worldLayout, midOfLayoutLength);
        rooms.Add(newRoom);

        Door parentDoor = parentRoom.Doors.Find(door => door.DoorId == parentRoomDoorId);

        

    }


    public void CreateWorldLayout()
    {
        Room startRoom = new Room1X1("StartRoom", 1, 1, 1);
        startRoom.Position = new Tuple<int, int>(0, 0);
        startRoom.MarkRoomInLayout(worldLayout, midOfLayoutLength);
        rooms.Add(startRoom);

        for (int i = 0; i < maxRoomNumbers - 1; i++)
        {
            AddRoomToLayout();
        }

    }


}
