using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = System.Random;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    GameObject player;
    Dictionary<Room, int> roomCounter = new Dictionary<Room, int>();
    GameObject startRoom;
    int nextRoomId = 0;
    List<Room> worldRooms = new List<Room>();

    //2D array for tracking position and doors of room cells
    private GridPosdataType[,] worldLayout;
    //2D array for tracking aval positions in the grid
    private bool[,] availableGridPositions;

    public const int roomBaseLength = 5;
    // world base length should be odd
    public const int worldBaseLength = 111;

    [SerializeField] public int maxRooms = 3;
    [SerializeField] public bool TestGeneration;

    public AssetBundle assets = null;

    public string currentStageName = "forrest";

    public async Task ResetStage()
    {
        
        roomCounter = new Dictionary<Room, int>();
        startRoom = null;
        nextRoomId = 0;
        worldRooms = new List<Room>();
        worldLayout = null;
        availableGridPositions = null;
        AssetBundle.UnloadAllAssetBundles(false);
        DestroyAllGOS();
        await Timer(5000);
        CreateGame();
        
    }
    void DestroyAllGOS()
    {
        var gos = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var g in gos)
        {
            if (g.Equals(player.gameObject) || g.Equals(gameObject))
            {
                continue;
            }
            else
            {
                UnityEngine.GameObject.Destroy(g);
            }
        }
        
    }
    private void Start()
    {
        CreateGame();
    }
    public void CreateGame()
    {

        worldLayout = new GridPosdataType[worldBaseLength, worldBaseLength];
        availableGridPositions = new bool[worldBaseLength, worldBaseLength];
        assets = Utils.loadAssetPack(currentStageName + "_stage");
        var gos = GetPossibleRooms();
        gos.ForEach(item => roomCounter.Add(item.GetComponent<Room>(), 0));
        InitWorldLayout();
        
        CreateStage();




       
        if (!TestGeneration)
        {
            SetEveryRoomInvisable();
        }

        foreach (var room in worldRooms)
        {
            room.SetDoors();
            room.SetRoomToDoors();

        }
        foreach (var room in worldRooms)
        {
            room.ConnectDoors();
        }
        foreach (var item in worldRooms)
        {
            foreach (var door in item.doors)
            {
                if (door.GetComponent<Door>().ConnectedDoor == null)
                {
                    door.GetComponent<Door>().doorIsOpen = false;
                }
            }
           
        }
        SetEveryFreeDoorClosed();

    }

    void SetEveryFreeDoorClosed()
    {
        foreach (var room in worldRooms)
        {
            room.GetComponent<Room>().Test();
        }
    }

    void SetStartRoom()
    {

        GameObject startRoom = Utils.loadAssetFromAssetPack(assets, "Start");

        var startRoomGO = Instantiate(startRoom, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

        SetStartRoomStats(startRoomGO, true, true, true, true);

        worldRooms.Add(startRoomGO.GetComponent<Room>());
        this.startRoom = startRoomGO;
    }

    public GameObject[] GetRooms()
    {
        var roomObjects = GameObject.FindObjectsOfType<Room>().ToList().Select(test => test.gameObject).ToArray();
        return roomObjects;
    }

    private List<GameObject> GetPossibleRooms()
    {
        var gos = Utils.LoadAllAssetsOfAssetPack(assets).ToList().FindAll(item => item.CompareTag("Room"));
        return gos;
    }


    GameObject AddRoom()
    {
        Random random = new Random();

        foreach (var posRoom in GetPossibleRooms().Shuffle().Select(room => room.GetComponent<Room>()))
        {
            if (roomCounter[posRoom] < posRoom.maxOfThisRoom || posRoom.maxOfThisRoom == -1)
            {

                foreach (var room in GetRooms().ToList().Shuffle().Select(room => room.GetComponent<Room>()))
                {
                    room.SetDoors();

                    foreach (var door in room.doors.Shuffle().Select(door => door.GetComponent<Door>()))
                    {
                        if (door.ConnectedDoor == null)
                        {
                            Tuple<int, int> posPosition = null;
                            var xOffset = random.Next(0, posRoom.lenX / StageController.roomBaseLength) * StageController.roomBaseLength;
                            var yOffset = random.Next(0, posRoom.lenY / StageController.roomBaseLength) * StageController.roomBaseLength;

                            switch (door.direction)
                            {
                                case Door.Direction.East:
                                    posPosition = new Tuple<int, int>( ((int)room.gameObject.transform.position.x + StageController.roomBaseLength) - posRoom.GetIndexOfFirstXRoomCell(yOffset) * StageController.roomBaseLength, (int)room.gameObject.transform.position.y + yOffset);
                                    break;
                                case Door.Direction.South:
                                    posPosition = new Tuple<int, int>((int)room.gameObject.transform.position.x - xOffset, ((int)room.gameObject.transform.position.y - StageController.roomBaseLength) + posRoom.GetIndexOfFirstYRoomCell(xOffset) * StageController.roomBaseLength);
                                    break;
                                case Door.Direction.West:
                                    posPosition = new Tuple<int, int>((int)room.gameObject.transform.position.x - posRoom.GetXLength(yOffset), (int)room.gameObject.transform.position.y + yOffset);
                                    break;
                                case Door.Direction.North:
                                    posPosition = new Tuple<int, int>((int)room.gameObject.transform.position.x - xOffset, (int)room.gameObject.transform.position.y + posRoom.GetYLength(xOffset));
                                    break;

                            }
                            posRoom.RoomId = nextRoomId;

                            if (CheckPosition(posRoom, posPosition) is null)
                            {

                                continue;
                            }
                            else
                            {
                                var newRoom = PlaceRoom(posRoom, posPosition);
                                nextRoomId++;
                                roomCounter[posRoom]++;
                                worldRooms.Add(newRoom.GetComponent<Room>());
                                return posRoom.gameObject;
                            }
                        }
                    }

                }


            }

        }


        return null;

    }
    private GameObject PlaceRoom(Room room, Tuple<int, int> position)
    {
        var roomLayout = room.GetRoomLayout();
        var go = Instantiate(room.gameObject, new Vector3(position.Item1, position.Item2), new Quaternion(0, 0, 0, 0));
        var arrayPos = ConvertFromGridToArrayIndex(position);

        foreach (var pos in roomLayout)
        {
            if (pos.roomId < 0)
            {
                continue;
            }
            var x = arrayPos.Item1 + pos.xPos / roomBaseLength;
            var y = arrayPos.Item2 - pos.yPos / roomBaseLength;
            worldLayout[y, x].roomId = room.RoomId;
            worldLayout[y, x].hasEDoor = pos.hasEDoor;
            worldLayout[y, x].hasNDoor = pos.hasNDoor;
            worldLayout[y, x].hasSDoor = pos.hasSDoor;
            worldLayout[y, x].hasWDoor = pos.hasWDoor;
            availableGridPositions[y, x] = false;

        }
        return go;
    }
    public Tuple<int, int> ConvertFromGridToArrayIndex(Tuple<int, int> position)
    {
        Tuple<int, int> arrayPosition = null;
        for (int y = worldLayout.GetLowerBound(0); y < worldLayout.GetUpperBound(0); y++)
        {
            for (int x = worldLayout.GetLowerBound(1); x < worldLayout.GetUpperBound(1); x++)
            {
                if (worldLayout[y, x].xPos == position.Item1 && worldLayout[y, x].yPos == position.Item2)
                {
                    arrayPosition = new Tuple<int, int>(x, y);
                    break;
                }
            }
        }
        return arrayPosition;

    }
    public Tuple<int, int> ConvertFromArrayIndexToGrid(Tuple<int, int> arrayPosition)
    {
        return new Tuple<int, int>(worldLayout[arrayPosition.Item2, arrayPosition.Item1].xPos, worldLayout[arrayPosition.Item2, arrayPosition.Item1].yPos);
    }
    /// <summary>
    /// Checks if the room could be placed on the given startposition
    /// </summary>
    /// <param name="room">Room object of new room</param>
    /// <param name="position">starting position of new room (x,y) </param>
    /// <returns></returns>
    private GameObject CheckPosition(Room room, Tuple<int, int> position)
    {
        var roomLayout = room.GetRoomLayout();
        var arrayPos = ConvertFromGridToArrayIndex(position);
        foreach (var pos in roomLayout)
        {
            var x = arrayPos.Item1 + pos.xPos / roomBaseLength;
            var y = arrayPos.Item2 - pos.yPos / roomBaseLength;

            if(pos.roomId < 0)
            {
                continue;
            }
            if (!availableGridPositions[y, x])
            {
                return null;
            }
            

            if (x + 1 > 0 && x + 1 < worldLayout.GetLength(0))
            {
                if (pos.hasEDoor)
                {

                    if (worldLayout[y, x + 1].roomId > -1 && worldLayout[y, x + 1].hasWDoor == false)
                    {
                        return null;
                    }



                }
                else
                {

                    if (worldLayout[y, x + 1].roomId != room.RoomId && worldLayout[y, x + 1].hasWDoor == true)
                    {
                        return null;
                    }
                    

                }

            }

            if (x - 1 > 0 && x - 1 < worldLayout.GetLength(0))
            {
                if (pos.hasWDoor)
                {

                    if (worldLayout[y, x - 1].roomId > -1 && worldLayout[y, x - 1].hasEDoor == false)
                    {
                        return null;
                    }
                }
                else
                {
                    if (worldLayout[y, x - 1].roomId != room.RoomId && worldLayout[y, x - 1].hasEDoor == true)
                    {
                        return null;
                    }
                    
                }

            }

            if (y - 1 > 0 && y - 1 < worldLayout.GetLength(0))
            {
                if (pos.hasSDoor)
                {

                    if (worldLayout[y + 1, x].roomId > -1 && worldLayout[y + 1, x].hasNDoor == false)
                    {
                        return null;
                    }
                }
                else
                {
                    if (worldLayout[y + 1, x].roomId != room.RoomId && worldLayout[y + 1, x].hasNDoor == true)
                    {
                        return null;
                    }
                    
                }

            }
            if (y + 1 > 0 && y + 1 < worldLayout.GetLength(0))
            {
                if (pos.hasNDoor)
                {

                    if (worldLayout[y - 1, x].roomId > -1 && worldLayout[y - 1, x].hasSDoor == false)
                    {
                        return null;
                    }
                }
                else
                {
                    if (worldLayout[y - 1, x].roomId != room.RoomId && worldLayout[y - 1, x].hasSDoor == true)
                    {
                        return null;
                    }
                    
                }

            }


        }


        return room.gameObject;






    }
    void SetStartRoomStats(GameObject roomGO, bool hasED, bool hasWD, bool hasSD, bool hasND)
    {

        Room room = roomGO.GetComponent<Room>();
        room.RoomId = nextRoomId;

        availableGridPositions[(int)((room.transform.position.y / roomBaseLength) + (worldLayout.GetLength(0) / 2)), (int)((room.transform.position.x / roomBaseLength) + (worldLayout.GetLength(0) / 2))] = false;
        worldLayout[(int)((room.transform.position.y / roomBaseLength) + (worldLayout.GetLength(0) / 2)), (int)((room.transform.position.x / roomBaseLength) + (worldLayout.GetLength(0) / 2))].roomId = room.RoomId;
        worldLayout[(int)((room.transform.position.y / roomBaseLength) + (worldLayout.GetLength(0) / 2)), (int)((room.transform.position.x / roomBaseLength) + (worldLayout.GetLength(0) / 2))].hasEDoor = hasED;
        worldLayout[(int)((room.transform.position.y / roomBaseLength) + (worldLayout.GetLength(0) / 2)), (int)((room.transform.position.x / roomBaseLength) + (worldLayout.GetLength(0) / 2))].hasNDoor = hasND;
        worldLayout[(int)((room.transform.position.y / roomBaseLength) + (worldLayout.GetLength(0) / 2)), (int)((room.transform.position.x / roomBaseLength) + (worldLayout.GetLength(0) / 2))].hasSDoor = hasSD;
        worldLayout[(int)((room.transform.position.y / roomBaseLength) + (worldLayout.GetLength(0) / 2)), (int)((room.transform.position.x / roomBaseLength) + (worldLayout.GetLength(0) / 2))].hasWDoor = hasWD;

        nextRoomId++;
    }
    async void CreateStage()
    {
        Random random = new Random();
        var f = random.Next(maxRooms / 2, maxRooms);
        SetStartRoom();
        CreatePlayer();
        int counter = 0;
        while (worldRooms.Count() < maxRooms && counter < 100)
        {
            var room = AddRoom();

            if (room is null)
                counter++;


        }
        //var endRoom = CreateEndRoom();
        Debug.Log("Rooms: " + nextRoomId);

        Utils.PrintGridPosDataTypeMatrix(worldLayout);


    }

    private GameObject CreateEndRoom()
    {
        Random random = new Random();
        var endRoom = Utils.loadAssetFromAssetPack(assets, "End").GetComponent<Room>();
        

                foreach (var room in GetRooms().ToList().Shuffle().Select(room => room.GetComponent<Room>()))
                {
                    room.SetDoors();

                    foreach (var door in room.doors.Shuffle().Select(door => door.GetComponent<Door>()))
                    {
                        if (door.ConnectedDoor == null)
                        {
                            Tuple<int, int> posPosition = null;
                            var xOffset = random.Next(0, endRoom.lenX / StageController.roomBaseLength) * StageController.roomBaseLength;
                            var yOffset = random.Next(0, endRoom.lenY / StageController.roomBaseLength) * StageController.roomBaseLength;

                            switch (door.direction)
                            {
                                case Door.Direction.East:
                                    posPosition = new Tuple<int, int>(((int)room.gameObject.transform.position.x + StageController.roomBaseLength) - endRoom.GetIndexOfFirstXRoomCell(yOffset) * StageController.roomBaseLength, (int)room.gameObject.transform.position.y + yOffset);
                                    break;
                                case Door.Direction.South:
                                    posPosition = new Tuple<int, int>((int)room.gameObject.transform.position.x - xOffset, ((int)room.gameObject.transform.position.y - StageController.roomBaseLength) - endRoom.GetIndexOfFirstYRoomCell(xOffset) * StageController.roomBaseLength);
                                    break;
                                case Door.Direction.West:
                                    posPosition = new Tuple<int, int>((int)room.gameObject.transform.position.x - endRoom.GetXLength(yOffset), (int)room.gameObject.transform.position.y + yOffset);
                                    break;
                                case Door.Direction.North:
                                    posPosition = new Tuple<int, int>((int)room.gameObject.transform.position.x - xOffset, (int)room.gameObject.transform.position.y + endRoom.GetYLength(xOffset));
                                    break;

                            }
                    endRoom.RoomId = nextRoomId;

                            if (CheckPosition(endRoom, posPosition) is null)
                            {

                                continue;
                            }
                            else
                            {
                                var newRoom = PlaceRoom(endRoom, posPosition);
                                worldRooms.Add(newRoom.GetComponent<Room>());
                                return endRoom.gameObject;
                            }
                        }
                    }

                }


            


        return null;
    }

    async Task Timer(int delay)
    {
        await Task.Delay(delay);
    }
    void InitWorldLayout()
    {
        int gpx;
        int gpy;
        gpy = (worldLayout.GetLength(0) / 2) * roomBaseLength;
        for (int y = 0; y < worldLayout.GetLength(0); y++)
        {
            gpx = worldLayout.GetLength(0) / 2 * roomBaseLength * -1;
            for (int x = 0; x < worldLayout.GetLength(1); x++)
            {
                availableGridPositions[y,x] = true;
                worldLayout[y, x] = new GridPosdataType(gpx, gpy);
                gpx += roomBaseLength;
            }


            gpy -= roomBaseLength;
        }


    }
    public void SetEveryRoomInvisable()
    {
        var roomObjects = GameObject.FindObjectsOfType(typeof(Room)) as Room[];
        foreach (Room room in roomObjects)
        {
            foreach (Renderer r in room.GetComponentsInChildren<Renderer>())
            {
                if (room.RoomId != 0)
                    r.enabled = false;
            }
        }
    }
    void InstantiateAssetGroupOnZero(GameObject[] assets)
    {
        foreach (var item in assets)
        {
            var go = Instantiate(item, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        }


    }
    void CreatePlayer()
    {

        var playerAssetsFile = Utils.loadAssetPack("player");
        var playerAssets = Utils.LoadAllAssetsOfAssetPack(playerAssetsFile);
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("StartPosition").gameObject.transform.position;
        GameObject player;
        GameObject camera;
        foreach (var item in playerAssets)
        {

            if (item.CompareTag("MainCamera"))
            {
                camera = Instantiate(item);
            }
            else if (item.CompareTag("Player"))
            {
                if (this.player is null)
                {
                    player = Instantiate(item, playerPosition, new Quaternion(0, 0, 0, 0));
                    this.player = player;
                    var gameManager = GameObject.FindGameObjectWithTag("GameManager").gameObject;
                    var playerManager = gameManager.GetComponent<PlayerManager>();
                    if (playerManager.player == null)
                        playerManager.player = player;
                }
                else
                {
                    this.player.transform.position = playerPosition;
                }
            }
            else
            {
                var go = Instantiate(item, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            }

        }



    }
    void InstantiateAssetGroup(GameObject[] assets, Vector3 position)
    {
        foreach (var item in assets)
        {
            Instantiate(item, position, new Quaternion(0, 0, 0, 0));
        }
    }


}
