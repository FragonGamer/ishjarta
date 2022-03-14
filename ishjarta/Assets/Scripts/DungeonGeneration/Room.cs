using System;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public int RoomId;
    List<GameObject> doors = new List<GameObject>();
    Tuple<int, int> size;

    [SerializeField] public int lenX;
    [SerializeField] public int lenY;

    public GridPosdataType[,] roomLayout = new GridPosdataType[0,0];

    public GridPosdataType[,] GetRoomLayout()
    {
        roomLayout = new GridPosdataType[lenY / StageController.roomBaseLength, lenX / StageController.roomBaseLength];
        var x = 0;
        var y = 0;
        for (int i = 0; i < roomLayout.GetLength(0); i++)
        {
            
            for (int j = 0; j < roomLayout.GetLength(1); j++)
            {

                roomLayout[i, j] = new GridPosdataType(y,x);
                y += StageController.roomBaseLength;
            }

            y = 0;
            x -= StageController.roomBaseLength;
        }
        return roomLayout;
    }

    private void SetDoors(GridPosdataType pos)
    {
        
    }
    public void ToggleRoomState()
    {
        var renderer = GetComponentsInChildren<Renderer>();
        if (renderer != null)
            foreach (Renderer r in renderer) { r.enabled = !r.enabled; }
        

    }
    private void Start()
    {
       ConnectDoors();
    }
    public void ConnectDoors()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject go in gos)
        {
            if (go.GetComponent<Door>().room.RoomId == RoomId)
            {
                doors.Add(go);
            }
        }
        foreach (var item in doors)
        {
            item.GetComponent<Door>().AttachClosestDoor();
        }
    }

}
