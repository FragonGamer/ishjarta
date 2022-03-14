using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;
using Random = System.Random;

public class StageController : MonoBehaviour
{

    
    GameObject player;
    [SerializeField] GameObject startRoom;
    int nextRoomId = 0;

    //2D array for tracking position and doors of room cells
    GridPosdataType[,] worldLayout = new GridPosdataType[7,7];
    //2D array for tracking aval positions in the grid
    bool[,] availableGridPositions = new bool[7,7];

    public const int roomBaseLength = 7;

    public AssetBundle assets = null;

    
    


    private void Awake()
    {
        assets = loadAssetPack("rooms");
        
        InitWorldLayout();
        CreateStage();
        
        
        //Utils.PrintPosMatrix(worldLayout);
        //Utils.PrintMatrix(availableGridPositions);
        
        CreatePlayer();
        SetEveryRoomInvisable();
    }

    void SetStartRoom()
    {

        GameObject startRoom = loadAssetFromAssetPack(assets, "Start");
        
        var startRoomGO = Instantiate(startRoom,new Vector3(0,0,0),new Quaternion(0,0,0,0));

        SetRoomStats(startRoomGO,true,true,true,true);
        Utils.PrintPosMatrix(startRoomGO.GetComponent<Room>().GetRoomLayout());
      
        
    }

    public GameObject[] GetRooms()
    {
        var roomObjects = GameObject.FindObjectsOfType(typeof(Room)) as GameObject[];
        return roomObjects;
    }
    GameObject AddRoom()
    {
        Random random = new Random(); 
        // imports all room prefabs of stage
        var gos = LoadAllAssetsOfAssetPack(assets).ToList();
        GameObject start = loadAssetFromAssetPack(assets, "Start");;
        gos.Remove(start);

        var rooms = GetRooms();
        bool placmentSuccess = false;

        //while (!placmentSuccess)
        //{
        //    var rootRoom = rooms[random.Next(0, rooms.Length)];
        //    placmentSuccess = true;
       // }
        
        
        
        GameObject room = gos.ToArray()[random.Next(0,gos.Count())];
        
        
        var roomGO = Instantiate(room);
        SetRoomStats(roomGO,true,true,true,true);
        
        Utils.PrintPosMatrix(roomGO.GetComponent<Room>().GetRoomLayout());
        return roomGO;

    }

    
    void SetRoomStats(GameObject roomGO,bool hasED, bool hasWD, bool hasSD, bool hasND)
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
    

    void CreateStage()
    {
        SetStartRoom();
        AddRoom();
    }
    void InitWorldLayout()
    {
        for (int i = 0; i < availableGridPositions.GetLength(0); i++)
        {
            for (int j = 0; j < availableGridPositions.GetLength(1); j++)
            {
                availableGridPositions[i, j] = true;
            }
        }
        int gpx;
        int gpy;
        gpy = (worldLayout.GetLength(0) / 2) * roomBaseLength ;
        for (int y = 0; y < worldLayout.GetLength(0); y++)
        {
            gpx = worldLayout.GetLength(0) / 2 * roomBaseLength * -1;
            for (int x = 0; x < worldLayout.GetLength(1); x++)
            {
                worldLayout[y,x] = new GridPosdataType(gpx,gpy);
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
        var playerAssetsFile = AssetBundle.LoadFromFile(Path.Combine(Utils.GetAssetsDirectory(), "player"));
        var playerAssets = playerAssetsFile.LoadAllAssets<GameObject>();
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
                player = Instantiate(item, playerPosition, new Quaternion(0, 0, 0, 0));
                this.player = player;
                var gameManager = GameObject.FindGameObjectWithTag("GameManager").gameObject;
                var playerManager = gameManager.GetComponent<PlayerManager>();
                try
                {
                    if (playerManager.player == null)
                        playerManager.player = player;
                }
                catch (System.Exception)
                {

                    throw;
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
    GameObject[] LoadAllAssetsOfAssetPack(AssetBundle assetBundle)
    {
        var prefabs = assetBundle.LoadAllAssets<GameObject>();
        return prefabs;
    }
    GameObject loadAssetFromAssetPack(AssetBundle assetBundle, string asset)
    {
        var prefab = assetBundle.LoadAsset<GameObject>(asset);
        return prefab;
    }
    AssetBundle loadAssetPack(string assetPack)
    {
        var myLoadedAssetBundle
            = AssetBundle.LoadFromFile(Path.Combine(Utils.GetAssetsDirectory(), assetPack));
        if (myLoadedAssetBundle == null)
        {
            throw new System.Exception("Failed to load AssetBundle!");
        }

        return myLoadedAssetBundle;
    }

}
