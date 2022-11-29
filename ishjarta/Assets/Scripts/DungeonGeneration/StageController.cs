using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = System.Random;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class StageController : MonoBehaviour
{
    [SerializeField] public bool ItemRooms;
    [SerializeField] public int ItemRoomAmount;
    [SerializeField] public bool MerchantRooms;
    [SerializeField] public int MerchantRoomAmount;
    GameObject player;
    Dictionary<Room, int> roomCounter = new Dictionary<Room, int>();
    GameObject startRoom;
    public GameObject camera;
    GameObject HUD;
    int nextRoomId = 0;
    public bool CreateRooms = true;

    public List<Room> worldRooms { get; private set; } = new List<Room>();

    //2D array for tracking position and doors of room cells
    private GridPosdataType[,] worldLayout;

    //2D array for tracking aval positions in the grid
    private bool[,] availableGridPositions;

    public const int roomXBaseLength = 9;

    public const int roomYBaseLength = 5;

    // world base length should be odd
    public const int worldBaseLength = 111;

    private int maxRooms;
    [SerializeField] public bool TestGeneration;

    public AssetBundle assets { get; private set; }
    public AssetBundle enemyAssets { get; private set; }
    public List<LevelName> stageNames { get; private set; }
    private int currentStageCounter;
    public LevelName currentStageName { get; private set; }

    int SetMaxRooms()
    {
        float result;

        result = 3.3f * (currentStageCounter + 1) + 5;


        return Mathf.FloorToInt(result);
    }

    public void ResetStage()
    {
        DestroyAllGOS();
        roomCounter = new Dictionary<Room, int>();
        startRoom = null;
        nextRoomId = 0;
        worldRooms = new List<Room>();
        worldLayout = null;
        availableGridPositions = null;
        AssetBundle.UnloadAllAssetBundles(false);
        Debug.Log("Resetted Stage");
        CreateGame();
    }

    public void ReloadGame()
    {
        AssetBundle.UnloadAllAssetBundles(true);
        HUD.SetActive(false);
        camera.GetComponent<Camera>().nearClipPlane = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene);
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
                g.SetActive(false);
            }
        }

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

    private void Awake()
    {
        stageNames = new List<LevelName>();
        stageNames.AddRange(new LevelName[] { LevelName.forest, LevelName.forest, LevelName.end });
        currentStageCounter = 0;
        if (CreateRooms == true)
        {
            CreateGame();
        }
    }

    public void CreateGame()
    {
        currentStageName = stageNames.ToArray()[currentStageCounter];
        worldLayout = new GridPosdataType[worldBaseLength, worldBaseLength];
        availableGridPositions = new bool[worldBaseLength, worldBaseLength];
        assets = Utils.loadAssetPack($"stage/{currentStageName.ToString()}");
        enemyAssets = Utils.loadAssetPack($"enemies/{currentStageName.ToString()}");
        maxRooms = SetMaxRooms();
        var gos = GetPossibleRooms();
        gos.ForEach(item => roomCounter.Add(item.GetComponent<Room>(), 0));
        InitWorldLayout();
        InstantiatePlayer();
        CreateStage();


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
        if (!TestGeneration)
        {
            SetEveryRoomInvisible();
        }
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
        GameObject startRoom = Utils.loadAssetFromAssetPack(assets, "start");

        var startRoomGO = Instantiate(startRoom, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

        SetStartRoomStats(startRoomGO, true, true, true, true);

        worldRooms.Add(startRoomGO.GetComponent<Room>());
        this.startRoom = startRoomGO;
        startRoom.GetComponent<Room>().isEntered = true;
        startRoom.GetComponent<Room>().hasVisited = true;
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
                foreach (var room in (worldRooms.ToList().Shuffle().Select(room => room.GetComponent<Room>())))
                {
                    room.SetDoors();

                    foreach (var door in room.doors.Shuffle().Select(door => door.GetComponent<Door>()))
                    {
                        if (door.ConnectedDoor == null)
                        {
                            Tuple<int, int> posPosition = null;
                            var xOffset = random.Next(0, posRoom.lenX / StageController.roomXBaseLength) *
                                          StageController.roomXBaseLength;
                            var yOffset = random.Next(0, posRoom.lenY / StageController.roomYBaseLength) *
                                          StageController.roomYBaseLength;

                            switch (door.direction)
                            {
                                case Door.Direction.East:
                                    posPosition = new Tuple<int, int>(
                                        ((int)room.gameObject.transform.position.x + StageController.roomXBaseLength) -
                                        posRoom.GetIndexOfFirstXRoomCell(yOffset) * StageController.roomXBaseLength,
                                        (int)room.gameObject.transform.position.y + yOffset);
                                    break;
                                case Door.Direction.South:
                                    posPosition = new Tuple<int, int>(
                                        (int)room.gameObject.transform.position.x - xOffset,
                                        ((int)room.gameObject.transform.position.y - StageController.roomYBaseLength) +
                                        posRoom.GetIndexOfFirstYRoomCell(xOffset) * StageController.roomYBaseLength);
                                    break;
                                case Door.Direction.West:
                                    posPosition = new Tuple<int, int>(
                                        (int)room.gameObject.transform.position.x - posRoom.GetXLength(yOffset),
                                        (int)room.gameObject.transform.position.y + yOffset);
                                    break;
                                case Door.Direction.North:
                                    posPosition = new Tuple<int, int>(
                                        (int)room.gameObject.transform.position.x - xOffset,
                                        (int)room.gameObject.transform.position.y + posRoom.GetYLength(xOffset));
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

            var x = arrayPos.Item1 + pos.xPos / StageController.roomXBaseLength;
            var y = arrayPos.Item2 - pos.yPos / StageController.roomYBaseLength;
            worldLayout[y, x].roomId = room.RoomId;
            worldLayout[y, x].hasEDoor = pos.hasEDoor;
            worldLayout[y, x].hasNDoor = pos.hasNDoor;
            worldLayout[y, x].hasSDoor = pos.hasSDoor;
            worldLayout[y, x].hasWDoor = pos.hasWDoor;
            availableGridPositions[y, x] = false;
        }

        Vector3 posStart = startRoom.transform.position;
        Vector3 posRoom = go.transform.position;
        Room goRoom = go.GetComponent<Room>();
        goRoom.DistanceToStart = (int)Vector3.Distance(posStart, posRoom);

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
        return new Tuple<int, int>(worldLayout[arrayPosition.Item2, arrayPosition.Item1].xPos,
            worldLayout[arrayPosition.Item2, arrayPosition.Item1].yPos);
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
            var x = arrayPos.Item1 + pos.xPos / roomXBaseLength;
            var y = arrayPos.Item2 - pos.yPos / roomYBaseLength;

            if (pos.roomId < 0)
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

    GameObject AddSpecialRoom(string name,int amount)
    {
            
       
            for (int i = 0; i < amount; i++)
            {
                
                Random random = new Random();
                var itemroom = Utils.loadAssetFromAssetPack(assets, name);
                var posRoom = itemroom.GetComponent<Room>();


                foreach (var room in (worldRooms.ToList().Shuffle().Select(room => room.GetComponent<Room>())))
                {
                    if (room.CompareTag("SpecialRoom"))
                        continue;
                    room.SetDoors();


                    foreach (var door in room.doors.Shuffle().Select(door => door.GetComponent<Door>()))
                    {
                        if (door.ConnectedDoor == null)
                        {
                            Tuple<int, int> posPosition = null;
                            var xOffset = random.Next(0, posRoom.lenX / StageController.roomXBaseLength) *
                                          StageController.roomXBaseLength;
                            var yOffset = random.Next(0, posRoom.lenY / StageController.roomYBaseLength) *
                                          StageController.roomYBaseLength;

                            switch (door.direction)
                            {
                                case Door.Direction.East:
                                    posPosition = new Tuple<int, int>(
                                        ((int)room.gameObject.transform.position.x + StageController.roomXBaseLength) -
                                        posRoom.GetIndexOfFirstXRoomCell(yOffset) * StageController.roomXBaseLength,
                                        (int)room.gameObject.transform.position.y + yOffset);
                                    break;
                                case Door.Direction.South:
                                    posPosition = new Tuple<int, int>(
                                        (int)room.gameObject.transform.position.x - xOffset,
                                        ((int)room.gameObject.transform.position.y - StageController.roomYBaseLength) +
                                        posRoom.GetIndexOfFirstYRoomCell(xOffset) * StageController.roomYBaseLength);
                                    break;
                                case Door.Direction.West:
                                    posPosition = new Tuple<int, int>(
                                        (int)room.gameObject.transform.position.x - posRoom.GetXLength(yOffset),
                                        (int)room.gameObject.transform.position.y + yOffset);
                                    break;
                                case Door.Direction.North:
                                    posPosition = new Tuple<int, int>(
                                        (int)room.gameObject.transform.position.x - xOffset,
                                        (int)room.gameObject.transform.position.y + posRoom.GetYLength(xOffset));
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
                                worldRooms.Add(newRoom.GetComponent<Room>());
                                return newRoom;
                            }
                        }
                    }

                    
                }
            }

            return null;

    }
    void CreateSpecialrooms()
    {
        if (ItemRooms)
        {
            for (int i = 0; i < ItemRoomAmount; i++)
            {
                AddSpecialRoom("ItemRoom",ItemRoomAmount);
            }
        }

        if (MerchantRooms)
        {
            for (int i = 0; i < MerchantRoomAmount; i++)
            {
                AddSpecialRoom("MerchantRoom",MerchantRoomAmount);
            }
        }

    }

    void SetStartRoomStats(GameObject roomGO, bool hasED, bool hasWD, bool hasSD, bool hasND)
    {
        Room room = roomGO.GetComponent<Room>();
        room.RoomId = nextRoomId;

        availableGridPositions[(int)((room.transform.position.y / roomYBaseLength) + (worldLayout.GetLength(0) / 2)),
            (int)((room.transform.position.x / roomXBaseLength) + (worldLayout.GetLength(0) / 2))] = false;
        worldLayout[(int)((room.transform.position.y / roomYBaseLength) + (worldLayout.GetLength(0) / 2)),
            (int)((room.transform.position.x / roomXBaseLength) + (worldLayout.GetLength(0) / 2))].roomId = room.RoomId;
        worldLayout[(int)((room.transform.position.y / roomYBaseLength) + (worldLayout.GetLength(0) / 2)),
            (int)((room.transform.position.x / roomXBaseLength) + (worldLayout.GetLength(0) / 2))].hasEDoor = hasED;
        worldLayout[(int)((room.transform.position.y / roomYBaseLength) + (worldLayout.GetLength(0) / 2)),
            (int)((room.transform.position.x / roomXBaseLength) + (worldLayout.GetLength(0) / 2))].hasNDoor = hasND;
        worldLayout[(int)((room.transform.position.y / roomYBaseLength) + (worldLayout.GetLength(0) / 2)),
            (int)((room.transform.position.x / roomXBaseLength) + (worldLayout.GetLength(0) / 2))].hasSDoor = hasSD;
        worldLayout[(int)((room.transform.position.y / roomYBaseLength) + (worldLayout.GetLength(0) / 2)),
            (int)((room.transform.position.x / roomXBaseLength) + (worldLayout.GetLength(0) / 2))].hasWDoor = hasWD;

        nextRoomId++;
    }

    void CreateStage()
    {
        //Creates Start room
        SetStartRoom();
        //Creates player
        CreatePlayer();
        int counter = 0;
        //creates normal rooms
        while (worldRooms.Count() < maxRooms && counter < 100)
        {
            var room = AddRoom();

            if (room is null)
                counter++;
        }

        //creates end room
        var endRoom = CreateEndRoom();
        //Creates special rooms
        CreateSpecialrooms();
        worldRooms.Add(endRoom.GetComponent<Room>());

        Debug.Log("Rooms: " + nextRoomId);
        currentStageCounter++;
        startRoom.GetComponent<Room>().hasVisited = true;
    }

    private GameObject CreateEndRoom()
    {
        Random random = new Random();
        var endRoom = Utils.loadAssetFromAssetPack(assets, "end").GetComponent<Room>();


        foreach (var room in worldRooms.ToList().OrderByDescending(room => room.DistanceToStart)
                     .Select(room => room.GetComponent<Room>()))
        {
            room.SetDoors();

            foreach (var door in room.doors.Shuffle().Select(door => door.GetComponent<Door>()))
            {
                if (door.ConnectedDoor == null)
                {
                    Tuple<int, int> posPosition = null;
                    var xOffset = random.Next(0, endRoom.lenX / StageController.roomXBaseLength) *
                                  StageController.roomXBaseLength;
                    var yOffset = random.Next(0, endRoom.lenY / StageController.roomYBaseLength) *
                                  StageController.roomYBaseLength;

                    switch (door.direction)
                    {
                        case Door.Direction.East:
                            posPosition = new Tuple<int, int>(
                                ((int)room.gameObject.transform.position.x + StageController.roomXBaseLength) -
                                endRoom.GetIndexOfFirstXRoomCell(yOffset) * StageController.roomXBaseLength,
                                (int)room.gameObject.transform.position.y + yOffset);
                            break;
                        case Door.Direction.South:
                            posPosition = new Tuple<int, int>((int)room.gameObject.transform.position.x - xOffset,
                                ((int)room.gameObject.transform.position.y - StageController.roomYBaseLength) -
                                endRoom.GetIndexOfFirstYRoomCell(xOffset) * StageController.roomYBaseLength);
                            break;
                        case Door.Direction.West:
                            posPosition =
                                new Tuple<int, int>(
                                    (int)room.gameObject.transform.position.x - endRoom.GetXLength(yOffset),
                                    (int)room.gameObject.transform.position.y + yOffset);
                            break;
                        case Door.Direction.North:
                            posPosition = new Tuple<int, int>((int)room.gameObject.transform.position.x - xOffset,
                                (int)room.gameObject.transform.position.y + endRoom.GetYLength(xOffset));
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

    void InitWorldLayout()
    {
        int gpx;
        int gpy;
        gpy = (worldLayout.GetLength(0) / 2) * roomYBaseLength;
        for (int y = 0; y < worldLayout.GetLength(0); y++)
        {
            gpx = worldLayout.GetLength(0) / 2 * roomXBaseLength * -1;
            for (int x = 0; x < worldLayout.GetLength(1); x++)
            {
                availableGridPositions[y, x] = true;
                worldLayout[y, x] = new GridPosdataType(gpx, gpy);
                gpx += roomXBaseLength;
            }


            gpy -= roomYBaseLength;
        }
    }

    public void SetEveryRoomInvisible()
    {
        var roomObjects = FindObjectsOfType<Room>().Where(room => room.RoomId != startRoom.GetComponent<Room>().RoomId);
        foreach (Room room in roomObjects)
        {
            room.gameObject.SetActive(!room.gameObject.active);
        }
    }

    Dictionary<string, GameObject> InstantiateAssetGroupOnZero(GameObject[] assets)

    {
        var result = new Dictionary<string, GameObject>();
        foreach (var item in assets)
        {
            var go = Instantiate(item, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            result.Add(go.name, go);
        }

        return result;
    }

    void InstantiatePlayer()
    {
        if (this.player is null)
        {
            var playerAssetsFile = Utils.loadAssetPack("player");
            var item = Utils.loadAssetFromAssetPack(playerAssetsFile, "Player");
            player = Instantiate(item, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            this.player = player;
            var gameManager = GameObject.FindGameObjectWithTag("GameManager").gameObject;
            var playerManager = gameManager.GetComponent<PlayerManager>();
            if (playerManager.player == null)
                playerManager.player = player;
            playerAssetsFile.Unload(false);
        }
    }

    void CreatePlayer()
    {
        var playerAssetsFile = Utils.loadAssetPack("player");
        var playerAssets = Utils.LoadAllAssetsOfAssetPack(playerAssetsFile);
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("StartPosition").gameObject.transform.position;
        if (player == null)
            InstantiatePlayer();

        playerAssets = playerAssets.ToList().FindAll(item => item.CompareTag("Player") == false).ToArray();


        this.player.transform.position = playerPosition;

        var gos = InstantiateAssetGroupOnZero(playerAssets);
        camera = gos.Where(x => x.Key.ToLower().Contains("camera") && x.Key.ToLower().Contains("main"))
            .Select(x => x.Value).First();
        HUD = gos.Where(x => x.Key.ToLower().Contains("hud")).Select(x => x.Value).First();
        playerAssetsFile.Unload(false);
        if (startRoom != null)
            player.GetComponent<Player>().currentRoom = startRoom.GetComponent<Room>();
    }

    Dictionary<string, GameObject> InstantiateAssetGroup(GameObject[] assets, Vector3 position)
    {
        var result = new Dictionary<string, GameObject>();
        foreach (var item in assets)
        {
            var go = Instantiate(item, position, new Quaternion(0, 0, 0, 0));
            result.Add(go.name, go);
        }

        return result;
    }
}