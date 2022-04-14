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

    /// <summary>
    /// removes enemy of enemies list in room
    /// </summary>
    /// <param name="enemy">enemy which you want to be removed</param>
    public void RemoveEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
    }
    private void Start()
    {
        //Gets all enemies in the current room
        Enemies = GetComponentsInChildren<Enemy>().ToList();
        SetCleared();
    }

    /// <summary>
    /// If the List of enemies in the room is empty (for example if all enemies are dead) the cleared status will be set to true
    /// </summary>
    public void SetCleared()
    {
        if (Enemies.Count == 0)
            IsCleared = true;
        else
        {
            IsCleared = false;
        }
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

    /// <summary>
    /// With this method all enemies in the room will be set to active (Enemies are disabled when you are not in the room)
    /// </summary>
    private void ActivateEnemies()
    {
        foreach (var enemy in Enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// sets all doors in room to be closed
    /// </summary>
    private void CloseRoom()
    {
        foreach (Door door in doors.Where(door => door.GetComponent<Door>().ConnectedDoor != null).Select(go => go.GetComponent<Door>()))
        {
            door.doorIsOpen = false;
        }
    }
    /// <summary>
    /// sets all doors in room to be opened
    /// </summary>
    private void OpenRoom()
    {
        foreach (Door door in doors.Where(door => door.GetComponent<Door>().ConnectedDoor != null).Select(go => go.GetComponent<Door>()))
        {
            door.doorIsOpen = true;
        }
    }
    /// <summary>
    /// gets the layout of the room with gridposdatatype
    /// 
    /// for example 5*10 room
    /// 
    /// output:
    /// 
    /// ( (0/0) hasSDoor=false hasNDoor = true hasWDoor = true hasEDoor = true roomId = 1)
    /// ( (0/5) hasSDoor=true hasNDoor = false hasWDoor = true hasEDoor = true roomId = 1 )
    /// 
    /// </summary>
    /// <returns>GridPosDataType array of the current room layout</returns>
    public GridPosdataType[,] GetRoomLayout()
    {

        roomLayout = new GridPosdataType[lenY / StageController.roomYBaseLength, lenX / StageController.roomXBaseLength];
        var x = 0;
        var y = 0;
        var tilemap = this.gameObject.GetComponentsInChildren<Tilemap>().ToList().Find(comp => comp.name.Contains("Background"));

        for (int i = 0; i < roomLayout.GetLength(0); i++)
        {
            for (int j = 0; j < roomLayout.GetLength(1); j++)
            {
                roomLayout[i, j] = new GridPosdataType(x, y);
                var tile = tilemap.GetTile(new Vector3Int(x + 4, y - 2, 0));
                if (tile != null)
                {


                    if (doors.Count > 0)
                    {
                        CalcDoorsOfRoomCell(this.gameObject, new Tuple<int, int>(i, j), x, y);
                    }
                    else
                    {
                        SetDoors();
                        CalcDoorsOfRoomCell(this.gameObject, new Tuple<int, int>(i, j), x, y);
                    }
                }

                x += StageController.roomXBaseLength;
            }

            x = 0;
            y -= StageController.roomYBaseLength;
        }

        return roomLayout;
    }

    /// <summary>
    /// gets the index of the first x value in an given y row
    /// </summary>
    /// <param name="offset">y offset of stagecontroller addroom</param>
    /// <returns>integer of index</returns>
    public int GetIndexOfFirstXRoomCell(int offset)
    {
        if (roomLayout is null) roomLayout = GetRoomLayout();
        offset /= StageController.roomYBaseLength;
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
    /// <summary>
    /// gets the index of the first y value in an given x row
    /// </summary>
    /// <param name="offset">x offset of stagecontroller addroom</param>
    /// <returns>integer of index</returns>
    public int GetIndexOfFirstYRoomCell(int offset)
    {
        if (roomLayout is null) roomLayout = GetRoomLayout();
        offset /= StageController.roomXBaseLength;
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
    /// <summary>
    /// gets the length of given x row
    /// </summary>
    /// <param name="offset">x offset of stagecontroller addroom</param>
    /// <returns>integer of length</returns>
    public int GetXLength(int offset)
    {
        if (roomLayout is null) roomLayout = GetRoomLayout();
        offset /= StageController.roomYBaseLength;
        int len = 0;

        for (int i = 0; i < roomLayout.GetLength(1); i++)
        {
            if (roomLayout[offset, i].roomId >= 0)
            {
                len += StageController.roomXBaseLength;
            }
        }
        return len; ;

    }
    /// <summary>
    /// gets the length of given y row
    /// </summary>
    /// <param name="offset">y offset of stagecontroller addroom</param>
    /// <returns>integer of length</returns>
    public int GetYLength(int offset)
    {
        if (roomLayout is null) roomLayout = GetRoomLayout();
        offset /= StageController.roomXBaseLength;
        int len = 0;

        for (int i = 0; i < roomLayout.GetLength(0); i++)
        {
            if (roomLayout[i, offset].roomId >= 0)
            {
                len += StageController.roomYBaseLength;
            }
        }
        return len;

    }
    /// <summary>
    /// calculates the doors in an given roomcell (gridposdatatype) of room
    /// </summary>
    /// <param name="go">gameobject (room)</param>
    /// <param name="gridPosition">position in the grid of the room</param>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    private void CalcDoorsOfRoomCell(GameObject go, Tuple<int, int> gridPosition, int x, int y)
    {
        GameObject helper = new GameObject();
        helper.transform.position = go.transform.position + new Vector3(Mathf.Floor(StageController.roomXBaseLength / 2), Mathf.Floor(StageController.roomYBaseLength / 2) * -1, 0) + new Vector3(x, y, 0);
        Vector3 helperPosition = helper.transform.position;
        List<Door> doorsOfThisCell = new List<Door>();
        var len = StageController.roomXBaseLength < StageController.roomYBaseLength ? StageController.roomXBaseLength : StageController.roomYBaseLength;
        foreach (var door in doors)
        {
            var diff = door.transform.position - helperPosition;
            float curDistance = diff.magnitude;
            if (curDistance <= len)
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

    /// <summary>
    /// toggles the state of the activness of the room gameobject
    /// </summary>
    public void ToggleRoomState()
    {

        isEntered = !isEntered;
        if (!FindObjectOfType<StageController>().GetComponent<StageController>().TestGeneration)
        {


            this.gameObject.SetActive(!this.gameObject.active);
        }


    }

    /// <summary>
    /// swaps the tiles of doors which are not connected to an other door
    /// </summary>
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

    /// <summary>
    /// adds all doors of room to the door list of the room
    /// </summary>
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

    /// <summary>
    /// sets the room to all doors in doors list
    /// </summary>
    public void SetRoomToDoors()
    {

        foreach (var door in doors)
        {
            door.GetComponent<Door>().room = gameObject.GetComponent<Room>();
        }


    }

    /// <summary>
    /// connects door to door of the nearest room
    /// </summary>
    public void ConnectDoors()
    {

        foreach (var item in doors)
        {
            item.GetComponent<Door>().AttachClosestDoor();

        }
    }

}
