using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;

public class StageController : MonoBehaviour
{

    [SerializeField] Room[] rooms;
    GameObject player;
    int baseRoomSize = 7;
    Tuple<int, int>[,] grid = new Tuple<int, int>[9, 9];
    bool[,] gridAval = new bool[9, 9];
    Vector3 baseCoordinates = new Vector3(0, 0, 0);
    public bool test = true;
    [SerializeField] GameObject startRoom;
    int nextRoomId = 1;
    Tuple<int, int> startPos;
    List<Room> roomList = new List<Room>();



    private void Awake()
    {
        rooms = GameObject.FindObjectsOfType(typeof(Room)) as Room[];
        CalcGrid();
        CreateStage();
        CreatePlayer();
        
        SetRoomNums();

        SetEveryRoomInvisable();
    }
    void SetStartRoom(){
        var level = loadAssetPack("level");
        GameObject startposition = loadAssetFromAssetPack(level,"StartPosition");

        var start = Instantiate(startposition,rooms[0].transform.position,new Quaternion(0,0,0,0));
        start.transform.position = rooms[0].gameObject.transform.position;
        start.transform.parent=rooms[0].gameObject.transform;
    }
    void CalcGrid()
    {
        int girdLength = grid.GetLength(0);
        int midPoint = girdLength / 2;
        for (int i = 0; i < girdLength; i++)
        {
            for (int j = 0; j < girdLength; j++)
            {
                Tuple<int, int> pair = new Tuple<int, int>(i * baseRoomSize, j * baseRoomSize);
                grid[i, j] = pair;
                gridAval[i, j] = false;
            }

        }
        startPos = grid[midPoint, midPoint];

    }
    void CreateStage()
    {

    }

    public void SetEveryRoomInvisable()
    {
        foreach (Room room in rooms)
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
    //Temp
    void SetRoomNums()
    {
        var rooms = GameObject.FindObjectsOfType<Room>();
        var startRoom = GameObject.FindGameObjectWithTag("StartPosition").GetComponent<StartPosition>().room;
        foreach (var room in rooms)
        {
            if (room != startRoom)
            {
                room.RoomId = nextRoomId++;
            }
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
