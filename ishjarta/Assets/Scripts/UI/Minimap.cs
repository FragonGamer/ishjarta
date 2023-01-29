using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
public struct ColorData{
    public ColorData(float hue,float saturation,float value){
        this._hue=hue;
        this._saturation=saturation;
        this._value=value;
        modifier=1.0f;
    }
    private float _hue;
    public float hue{
        set => _hue=value;
        get => Convert.ToSingle(_hue/360);
    }
    private float _saturation;
    public float saturation
    {
        set => _saturation = value;
        get => Convert.ToSingle(_saturation/100);
    }
    private float _value;
    public float value{
        set => _value = value;
        get => Convert.ToSingle(_value/100);
    }

    public float modifier;
    public override string ToString() => $"(HUE:{hue}, SATURATION:{saturation}, VALUE:{value}, MODIFIER:{modifier})";

}
public class Minimap : MonoBehaviour
{
    [SerializeField] private Sprite minimapSprite;
    [SerializeField] private Grid minimapGrid;
    #region color
    // default color for rooms
    // wall colors
    // standard room
    [SerializeField]private ColorData lightGrey= new ColorData(0,0,70);
    // item rooms
    [SerializeField]private ColorData yellow = new ColorData(60,100,70);
    // floor colors
    [SerializeField]private ColorData grey = new ColorData(0,0,40);
    // modifiers
    private float darkModifier = 0.3f;
    private float lightModifier = 1.5f;
    #endregion
    private List<Tuple<int, Tilemap,ColorData>> tilemaps = new List<Tuple<int, Tilemap,ColorData>>();
    private List<Tuple<int, GameObject, Room>> folders = new List<Tuple<int, GameObject, Room>>();
    private List<Room> rooms = new List<Room>();
    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void UpdateColors()
    {

        foreach (var tilemapdata in tilemaps)
        {
            var tilemap = tilemapdata.Item2;
            var colorData = tilemapdata.Item3;
            if (!tilemap.gameObject.transform.parent.gameObject.active)
            {
                continue;
            }
            var room = rooms.Where(r => r.RoomId == tilemapdata.Item1).First();
            colorData.modifier = 1.0f;
            grey.modifier = 1.0f;
            if (player.currentRoom.RoomId == tilemapdata.Item1)
            {
                colorData.modifier = lightModifier;
                grey.modifier = lightModifier;
            }
            else if (!room.hasVisited)
            {
                colorData.modifier = darkModifier;
                grey.modifier = darkModifier;
            }
            if (tilemap.gameObject.name.ToLower().Contains("back"))
            {
                tilemap.color = Color.HSVToRGB(grey.hue,grey.saturation,grey.value * grey.modifier);
                
            }
            else if (tilemap.gameObject.name.ToLower().Contains("obstacle"))
            {
                tilemap.color = Color.HSVToRGB(colorData.hue,colorData.saturation,colorData.value * colorData.modifier);
                

            }
            
        }

    }

    private List<Room>GetConnectedRooms(Room room){
        List<Door> doors = room.doors.Where(d => d.GetComponent<Door>() != null).Select(d => d.GetComponent<Door>()).ToList();
        List<Room> connectedRooms = doors.Where(d => d.ConnectedDoorRoom != null).Select(d => d.ConnectedDoorRoom).ToList(); 
        return connectedRooms;
    }

    private void UpdateRooms()
    {


        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        foreach (Room room in rooms)
        {
            var folder = folders.Where(r => r.Item1 == room.RoomId).Select(r => r.Item2).FirstOrDefault();
            if (folder == null)
                continue;
            if (room.hasVisited)
            {
                folder.gameObject.SetActive(true);
                var rooms = GetConnectedRooms(room);
                foreach(Room r in rooms){
                    if (r == null)
                        continue;
                    var roomFolder = folders.Where(f => f.Item1 == r.RoomId).Select(f => f.Item2).FirstOrDefault();
                    if(roomFolder != null && !roomFolder.active){
                        roomFolder.SetActive(true);
                    }
                }
            }
            






        }
    }

    public void UpdateMinimap()
    {
        UpdateRooms();
        UpdateColors();
    }
    public void AddRoomsToMinimap(List<Room> rooms)
    {
        foreach (var room in rooms)
        {
            AddRoomToMinmap(room);
        }
    }
    public void AddRoomToMinmap(Room room)
    {
        rooms.Add(room);
        var tileobj = new Tile();
        tileobj.sprite = minimapSprite;
        var folder = new GameObject($"Room {room.RoomId}");
        folder.transform.SetParent(minimapGrid.transform);
        folder.layer = 7;
        var tilemapObjects = room.GetComponentInChildren<Grid>().gameObject.transform;
        foreach (Transform tmo in tilemapObjects)
        {

            var newtmo = Instantiate(tmo.gameObject, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            var tilemap = newtmo.GetComponent<Tilemap>();
            var tilecollider = newtmo.GetComponent<TilemapCollider2D>();
            if (tilecollider != null)
                Destroy(tilecollider);
            foreach (TileData tile in tilemap.GetAllTiles())
            {
                tilemap.SetTile(new Vector3Int(tile.X, tile.Y), tileobj);
            }

            newtmo.layer = 7;

            newtmo.transform.SetParent(folder.transform);
            ColorData colorData;
            
            if(room.gameObject.name.ToLower().Contains("itemroom")){
                colorData = yellow;
            }
            else{
                colorData = lightGrey;
            }

            tilemaps.Add(new Tuple<int, Tilemap,ColorData>(room.RoomId, tilemap,colorData));

        }
        folder.transform.position = new Vector3(room.position.Item1, room.position.Item2, 0);
        folders.Add(new Tuple<int, GameObject, Room>(room.RoomId, folder, room));
        folder.SetActive(false);


    }

}
