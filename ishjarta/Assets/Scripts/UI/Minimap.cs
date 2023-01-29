using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Sprite minimapSprite;
    [SerializeField] private Grid minimapGrid;
    #region color
    // default color for rooms
    // wall colors
    // standard room
    private Color lightgrey = new Color(0.7f,0.7f,0.7f);
    // item rooms
    private Color yellow = new Color(1,0.92f,0.016f);
    // floor colors
   private Color grey = new Color(0.5f,0.5f,0.5f);
    // color multiplier
    // color for entered room
    private Color light = new Color(0.9f,0.9f,0.9f);
    // color for discovered room
    // color for surrounded but not discovered room
    private Color dark = new Color(0.1f,0.1f,0.1f);
    #endregion
    private List<Tuple<int,Tilemap>> tilemaps = new List<Tuple<int,Tilemap>>();
    private List<Tuple<int,GameObject,Room>> folders = new List<Tuple<int,GameObject,Room>>();
    private List<Room> rooms = new List<Room>();
    private Player player;

void Start()
{
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
}
    private void UpdateColors(){
        
        foreach(var tilemapdata in tilemaps){
            var tilemap = tilemapdata.Item2;
            if(!tilemap.gameObject.transform.parent.gameObject.active){
                continue;
            }
                var room = rooms.Where(r => r.RoomId == tilemapdata.Item1).First();
            if(tilemap.gameObject.name.ToLower().Contains("back")){
                tilemap.color=grey;
            }
            else if( tilemap.gameObject.name.ToLower().Contains("obstacle")){
                switch(room.gameObject.name){
                    case "ItemRoom":
                        tilemap.color = yellow;
                        break;   
                    default:
                        tilemap.color = lightgrey;
                        break;

                }
            }
            if(player.currentRoom.RoomId == tilemapdata.Item1){
                tilemap.color = tilemap.color * light;
            }
            else if(!room.hasVisited){
                tilemap.color = tilemap.color * dark;
            }
        }

    }

    private void UpdateRooms(){
        
        
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        foreach(Room room in rooms){
            var folder = folders.Where(r => r.Item1 == room.RoomId).Select(r => r.Item2).FirstOrDefault();
            if(folder == null)
                continue;
            if(room.hasVisited){
                folder.gameObject.SetActive(true); 
            }
          

            
            

        }
    }
   
    public void UpdateMinimap(){
        UpdateRooms();
        UpdateColors();
    }
    public void AddRoomsToMinimap(List<Room> rooms){
        foreach(var room in rooms){
            AddRoomToMinmap(room);
        }
    }
    public void AddRoomToMinmap(Room room){
        rooms.Add(room);
        var tileobj = new Tile();
        tileobj.sprite = minimapSprite;
        var folder = new GameObject($"Room {room.RoomId}");
        folder.transform.SetParent(minimapGrid.transform);
        folder.layer = 7;
        var tilemapObjects = room.GetComponentInChildren<Grid>().gameObject.transform;
        foreach(Transform tmo in tilemapObjects){
            
            var newtmo = Instantiate(tmo.gameObject,new Vector3(0,0,0),new Quaternion(0,0,0,0));
            var tilemap = newtmo.GetComponent<Tilemap>();
            var tilecollider = newtmo.GetComponent<TilemapCollider2D>();
            if(tilecollider != null)
                Destroy(tilecollider);
            foreach(TileData tile in tilemap.GetAllTiles()){
                tilemap.SetTile(new Vector3Int(tile.X,tile.Y),tileobj);
            }
            
            newtmo.layer = 7;
           
            newtmo.transform.SetParent(folder.transform);
            tilemaps.Add(new Tuple<int, Tilemap>(room.RoomId,tilemap));

        }
        folder.transform.position = new Vector3 (room.position.Item1,room.position.Item2,0);
        folders.Add(new Tuple<int,GameObject,Room>(room.RoomId,folder,room));
        folder.SetActive(false);


    }

}
