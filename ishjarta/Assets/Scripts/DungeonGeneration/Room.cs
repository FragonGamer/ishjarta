using System;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public int RoomId;
    public List<GameObject> doors { get; private set;} = new List<GameObject>();

    [SerializeField] public int lenX;
    [SerializeField] public int lenY;

    private GridPosdataType[,] roomLayout = null;

    


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
                if(doors.Count > 0){
                    CalcDoorsOfRoomCell(this.gameObject,new Tuple<int, int>(i,j),y,x);
                }
                else{
                    SetDoors();
                    CalcDoorsOfRoomCell(this.gameObject,new Tuple<int, int>(i,j),y,x);
                }
                y += StageController.roomBaseLength;
            }

            y = 0;
            x -= StageController.roomBaseLength;
        }
        
        return roomLayout;
    }

    private void CalcDoorsOfRoomCell(GameObject go, Tuple<int,int> gridPosition,int x,int y){
        GameObject helper = new GameObject();
        helper.transform.position = go.transform.position + new Vector3(-3.5f,4.5f,0) + new Vector3(x,y,0);
        Vector3 helperPosition = helper.transform.position;
        List<Door> doorsOfThisCell = new List<Door>();

        foreach(var door in doors){
            var diff = door.transform.position - helperPosition;
            float curDistance = diff.sqrMagnitude;
            if(curDistance <= StageController.roomBaseLength){
                doorsOfThisCell.Add(door.GetComponent<Door>());
            }

        }
         roomLayout[gridPosition.Item1,gridPosition.Item2].roomId = RoomId;

        foreach(var door in doorsOfThisCell){
            switch(door.direction){
                case Door.Direction.East:
                    roomLayout[gridPosition.Item1,gridPosition.Item2].hasEDoor = true;
                    break;
                case Door.Direction.West:
                    roomLayout[gridPosition.Item1,gridPosition.Item2].hasWDoor = true;
                    break;
                case Door.Direction.North:
                    roomLayout[gridPosition.Item1,gridPosition.Item2].hasNDoor = true;
                    break;
                case Door.Direction.South:
                    roomLayout[gridPosition.Item1,gridPosition.Item2].hasSDoor = true;
                    break;
                default:
                break;
            }
        }
        Destroy(helper);


    }


    public void ToggleRoomState()
    {
        var renderer = GetComponentsInChildren<Renderer>();
        if (renderer != null)
            foreach (Renderer r in renderer) { r.enabled = !r.enabled; }
        
       
    }

    //This was for testing purposes of tilemap swap
    public void Test()
    {
        
            var test = this.gameObject.GetComponentsInChildren<Tilemap>();
            foreach (Tilemap tilemap in test)
            {
                if (tilemap.name.Contains("Obstacle"))
                {

                    foreach (var door in doors)
                    {
                        if (door.GetComponent<Door>().ConnectedDoor == null)
                        {
                            var tile1 = tilemap.GetTile(new Vector3Int(-4, 6));
                            tilemap.SetTile(new Vector3Int(Mathf.FloorToInt(door.transform.position.x), Mathf.FloorToInt(door.transform.position.y)), tile1);
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
    public void SetRoomToDoors(){

        foreach (var door in doors){
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
