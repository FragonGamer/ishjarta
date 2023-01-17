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
    private Color grey = new Color(0.5f,0.5f,0.5f);
    private Color lightgrey = new Color(0.7f,0.7f,0.7f);
    private Color darkgrey = new Color(0.3f,0.3f,0.3f);
    private Color black = new Color(0,0,0);
    private Color white= new Color(1,1,1);
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
            if(tilemap.name.ToLower().Contains("back")){
                tilemap.color = grey;
            }
            else if(player.currentRoom.RoomId == tilemapdata.Item1){
                tilemap.color = lightgrey;
            }
            else{
                var room = rooms.Where(r => r.RoomId == tilemapdata.Item1).First();
                switch(room.gameObject.tag){
                    case "ItemRoom":
                        tilemap.color = Color.yellow;
                        break;
                    default:
                        tilemap.color = darkgrey;
                        break;

                }
                
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
