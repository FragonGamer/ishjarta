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
    [SerializeField] GameObject startRoom;
    int nextRoomId = 1;

    //2D array for tracking position and doors of room cells
    GridPosdataType[,] worldLayout = new GridPosdataType[7,7];
    //2D array for tracking aval positions in the grid
    bool[,] availableGridPositions = new bool[7,7];

    int gridLength;
    int midLength;
    const int roomBaseLength = 7;



    private void Awake()
    {
        gridLength = worldLayout.GetLength(0);
        midLength = worldLayout.GetLength(0) / 2;
        rooms = GameObject.FindObjectsOfType(typeof(Room)) as Room[];
        initWorldLayout();
        Utils.PrintPosMatrix(worldLayout);
        //CreatePlayer();
        
        //SetRoomNums();

        //SetEveryRoomInvisable();
    }
    void initWorldLayout()
    {
        for (int i = 0; i < availableGridPositions.GetLength(0); i++)
        {
            for (int j = 0; j < availableGridPositions.GetLength(0); j++)
            {
                availableGridPositions[i, j] = false;
            }
        }
        for (int i = 0; i < worldLayout.GetLength(0); i++)
        {
            for (int j = 0; j < worldLayout.GetLength(0); j++)
            {
                worldLayout[i, j] = new GridPosdataType(0,0);
            }
        }
        for (int i = 0; i <= worldLayout.GetLength(0) / 2; i++)
        {
            for (int j = 0; j <= worldLayout.GetLength(0) / 2; j++)
            {
                worldLayout[i, j] = new GridPosdataType((roomBaseLength * (midLength - j)) , -(roomBaseLength * (midLength - i)));
            }
        }

        int tmp = 1;
        int tmp2;
        for (int i = 0; i < (worldLayout.GetLength(0) / 2) + 1; i++)
        {
            tmp2 = midLength;
            for (int j = (worldLayout.GetLength(0) / 2) + 1; j < worldLayout.GetLength(0); j++)
            {
                worldLayout[i, j] = new GridPosdataType(roomBaseLength * tmp, (tmp2 * roomBaseLength));
                tmp2--;
            }
            tmp++;
        }

        //for (int i = 0; i <= worldLayout.GetLength(0) / 2; i++)
        //{
        //    for (int j = (worldLayout.GetLength(0) / 2) + 1; j < worldLayout.GetLength(0); j++)
        //    {

        //        worldLayout[i, j] = new GridPosdataType((roomBaseLength * (midLength - i)), (roomBaseLength * (midLength - j)));
        //    }
        //}


        //tmp = roomBaseLength;
        //for (int i = (worldLayout.GetLength(0) / 2) + 1; i < worldLayout.GetLength(0); i++)
        //{
        //    tmp2 = -roomBaseLength;
        //    for (int j = (worldLayout.GetLength(0) / 2)+1; j < worldLayout.GetLength(0); j++)
        //    {
        //        worldLayout[i, j] = new GridPosdataType(tmp , tmp2);
        //        tmp2 = tmp2 + -roomBaseLength;
        //    }
        //    tmp += roomBaseLength;
        //}


    }
    void SetStartRoom(){
        var level = loadAssetPack("level");
        GameObject startposition = loadAssetFromAssetPack(level,"StartPosition");

        var start = Instantiate(startposition,rooms[0].transform.position,new Quaternion(0,0,0,0));
        start.transform.position = rooms[0].gameObject.transform.position;
        start.transform.parent=rooms[0].gameObject.transform;
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
