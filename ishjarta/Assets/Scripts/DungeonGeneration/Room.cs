using System;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class Room : MonoBehaviour
{

    [SerializeField] public int maxOfThisRoom;
    public bool IsCleared { get; private set; } = false;
    [SerializeField] public bool isEntered = false;
    public bool isClosed = false;
    public int RoomId;
    public int DistanceToStart = 0;
    [SerializeField] public List<Enemy> Enemies = new List<Enemy>();
    // public Door.Direction DirectionToStart { get; set; }
    public List<GameObject> doors { get; private set; } = new List<GameObject>();

    [SerializeField] public int lenX;
    [SerializeField] public int lenY;


    [Header("Tiles")]
    [SerializeField] Tile closedWallUp;
    [SerializeField] Tile closedWallDown;
    [SerializeField] Tile closedWallLeft;
    [SerializeField] Tile closedWallRight;

    private GridPosdataType[,] roomLayout = null;

    public void RemoveEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
    }
    private void Start()
    {
        Enemies = GetComponentsInChildren<Enemy>().ToList();
        if (Enemies.Count == 0)
            IsCleared = true;
    }
    private void Update()
    {
        if (isEntered)
        {

            if (!IsCleared)
            {

                if (!isClosed)
                {
                    CloseRoom();
                    ActivateEnemies();
                    isClosed = true;

                }

                if (Enemies.Count == 0)
                {
                    OpenRoom();
                    isClosed = false;
                    //Things after completed room here!!
                }


            }

        }
    }
    private void ActivateEnemies()
    {
        foreach (var enemy in Enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }
    private void CloseRoom()
    {
        foreach (Door door in doors.Where(door => door.GetComponent<Door>().ConnectedDoor != null).Select(go => go.GetComponent<Door>()))
        {
            door.doorIsOpen = false;
        }
    }
    private void OpenRoom()
    {
        foreach (Door door in doors.Where(door => door.GetComponent<Door>().ConnectedDoor != null).Select(go => go.GetComponent<Door>()))
        {
            door.doorIsOpen = true;
        }
    }

    public GridPosdataType[,] GetRoomLayout()
    {

        roomLayout = new GridPosdataType[lenY / StageController.roomBaseLength, lenX / StageController.roomBaseLength];
        var x = 0;
        var y = 0;
        var tilemap = this.gameObject.GetComponentsInChildren<Tilemap>().ToList().Find(comp => comp.name.Contains("Background"));

        for (int i = 0; i < roomLayout.GetLength(0); i++)
        {
            for (int j = 0; j < roomLayout.GetLength(1); j++)
            {
                roomLayout[i, j] = new GridPosdataType(y, x);
                if (tilemap.HasTile(new Vector3Int(y + 1, x - 1, 0)))
                {


                    if (doors.Count > 0)
                    {
                        CalcDoorsOfRoomCell(this.gameObject, new Tuple<int, int>(i, j), y, x);
                    }
                    else
                    {
                        SetDoors();
                        CalcDoorsOfRoomCell(this.gameObject, new Tuple<int, int>(i, j), y, x);
                    }
                }

                y += StageController.roomBaseLength;
            }

            y = 0;
            x -= StageController.roomBaseLength;
        }

        return roomLayout;
    }

    public int GetIndexOfFirstXRoomCell(int offset)
    {
        if (roomLayout is null) roomLayout = GetRoomLayout();
        offset /= StageController.roomBaseLength;
        int index = 0;
        for (int i = 0; i < roomLayout.GetLength(1); i++)
        {
            if (roomLayout[offset, i].roomId >= 0)
            {
                index = i;
                break;
            }
        }
        return index;

    }
    public int GetIndexOfFirstYRoomCell(int offset)
    {
        if (roomLayout is null) roomLayout = GetRoomLayout();
        offset /= StageController.roomBaseLength;
        int index = 0;
        for (int i = 0; i < roomLayout.GetLength(0); i++)
        {
            if (roomLayout[i, offset].roomId >= 0)
            {
                index = i;
                break;
            }
        }
        return index;

    }
    public int GetXLength(int offset)
    {
        if (roomLayout is null) roomLayout = GetRoomLayout();
        offset /= StageController.roomBaseLength;
        int len = 0;

        for (int i = 0; i < roomLayout.GetLength(1); i++)
        {
            if (roomLayout[offset, i].roomId >= 0)
            {
                len += StageController.roomBaseLength;
            }
        }
        return len; ;

    }
    public int GetYLength(int offset)
    {
        if (roomLayout is null) roomLayout = GetRoomLayout();
        offset /= StageController.roomBaseLength;
        int len = 0;

        for (int i = 0; i < roomLayout.GetLength(0); i++)
        {
            if (roomLayout[i, offset].roomId >= 0)
            {
                len += StageController.roomBaseLength;
            }
        }
        return len;

    }

    private void CalcDoorsOfRoomCell(GameObject go, Tuple<int, int> gridPosition, int x, int y)
    {
        GameObject helper = new GameObject();
        helper.transform.position = go.transform.position + new Vector3(2f, -2f, 0) + new Vector3(x, y, 0);
        Vector3 helperPosition = helper.transform.position;
        List<Door> doorsOfThisCell = new List<Door>();

        foreach (var door in doors)
        {
            var diff = door.transform.position - helperPosition;
            float curDistance = diff.sqrMagnitude;
            if (curDistance <= StageController.roomBaseLength)
            {
                doorsOfThisCell.Add(door.GetComponent<Door>());
            }

        }
        roomLayout[gridPosition.Item1, gridPosition.Item2].roomId = RoomId;

        foreach (var door in doorsOfThisCell)
        {
            switch (door.direction)
            {
                case Door.Direction.East:
                    roomLayout[gridPosition.Item1, gridPosition.Item2].hasEDoor = true;
                    break;
                case Door.Direction.West:
                    roomLayout[gridPosition.Item1, gridPosition.Item2].hasWDoor = true;
                    break;
                case Door.Direction.North:
                    roomLayout[gridPosition.Item1, gridPosition.Item2].hasNDoor = true;
                    break;
                case Door.Direction.South:
                    roomLayout[gridPosition.Item1, gridPosition.Item2].hasSDoor = true;
                    break;
                default:
                    break;
            }
        }
        Destroy(helper);


    }


    public void ToggleRoomState()
    {

        isEntered = !isEntered;
        if (!FindObjectOfType<StageController>().GetComponent<StageController>().TestGeneration)
        {


            this.gameObject.SetActive(!this.gameObject.active);
        }


    }

    //This was for testing purposes of tilemap swap
    public void Test()
    {

        var tilemaps = this.gameObject.GetComponentsInChildren<Tilemap>();
        foreach (Tilemap tilemap in tilemaps)
        {
            if (tilemap.name.Contains("Obstacle"))
            {

                foreach (var door in doors.Select(item => item.GetComponent<Door>()))
                {
                    if (door.ConnectedDoor == null)
                    {

                        switch (door.direction)
                        {
                            case Door.Direction.East:
                                tilemap.SetTile(new Vector3Int((int)door.gameObject.transform.localPosition.x, (int)door.gameObject.transform.localPosition.y), closedWallRight);
                                tilemap.SetTile(new Vector3Int((int)door.gameObject.transform.localPosition.x, (int)door.gameObject.transform.localPosition.y + 1), closedWallRight);
                                tilemap.SetTile(new Vector3Int((int)door.gameObject.transform.localPosition.x, (int)door.gameObject.transform.localPosition.y - 1), closedWallRight);
                                break;
                            case Door.Direction.South:
                                tilemap.SetTile(new Vector3Int((int)door.gameObject.transform.localPosition.x, (int)door.gameObject.transform.localPosition.y), closedWallDown);
                                tilemap.SetTile(new Vector3Int((int)door.gameObject.transform.localPosition.x + 1, (int)door.gameObject.transform.localPosition.y), closedWallDown);
                                tilemap.SetTile(new Vector3Int((int)door.gameObject.transform.localPosition.x - 1, (int)door.gameObject.transform.localPosition.y), closedWallDown);
                                break;
                            case Door.Direction.West:
                                tilemap.SetTile(new Vector3Int((int)door.gameObject.transform.localPosition.x, (int)door.gameObject.transform.localPosition.y), closedWallLeft);
                                tilemap.SetTile(new Vector3Int((int)door.gameObject.transform.localPosition.x, (int)door.gameObject.transform.localPosition.y + 1), closedWallLeft);
                                tilemap.SetTile(new Vector3Int((int)door.gameObject.transform.localPosition.x, (int)door.gameObject.transform.localPosition.y - 1), closedWallLeft);
                                break;
                            case Door.Direction.North:
                                tilemap.SetTile(new Vector3Int((int)door.gameObject.transform.localPosition.x, (int)door.gameObject.transform.localPosition.y), closedWallUp);
                                tilemap.SetTile(new Vector3Int((int)door.gameObject.transform.localPosition.x + 1, (int)door.gameObject.transform.localPosition.y), closedWallUp);
                                tilemap.SetTile(new Vector3Int((int)door.gameObject.transform.localPosition.x - 1, (int)door.gameObject.transform.localPosition.y), closedWallUp);
                                break;

                        }

                    }
                }


            }
        }

    }


    public void SetDoors()
    {

        var gos = this.GetComponentsInChildren<Door>();
        foreach (var item in gos)
        {
            if (!doors.Contains(item.gameObject))
            {
                doors.Add(item.gameObject);
            }
        }
    }
    public void SetRoomToDoors()
    {

        foreach (var door in doors)
        {
            door.GetComponent<Door>().room = gameObject.GetComponent<Room>();
        }


    }
    public void ConnectDoors()
    {

        foreach (var item in doors)
        {
            item.GetComponent<Door>().AttachClosestDoor();

        }
    }

}
