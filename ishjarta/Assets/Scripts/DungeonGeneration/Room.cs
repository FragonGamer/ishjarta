using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    [SerializeField] public int RoomId;
    List<GameObject> doors = new List<GameObject>();
    Tuple<int, int> size;

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
    void ConnectDoors()
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
