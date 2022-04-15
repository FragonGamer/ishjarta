using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;


public class Door : MonoBehaviour
{
    public enum Direction
    {
        West,
        East,
        North,
        South
    }

    public bool doorIsOpen;
    public GameObject ConnectedDoor = null;
    [SerializeField] public Direction direction;

    [SerializeField] public Tile closedDoorTile;

    public Room room;




    public Room ConnectedDoorRoom;


    private void Awake()
    {
        room = GetComponentInParent<Room>();
    }


    public void TeleportPlayerToDoor()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = ConnectedDoor.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doorIsOpen && collision.CompareTag("Player") && collision.GetType() == typeof(CircleCollider2D))
        {
            GameObject player = collision.gameObject;
            ConnectedDoorRoom.ToggleRoomState();
            TeleportPlayerToDoor();
            room.ToggleRoomState();
            var tilemap = room.gameObject.GetComponentsInChildren<Tilemap>().Where(x => x.name.ToLower().Contains("background")).First();
        }
    }

    public void AttachClosestDoor()
    {
        var door = FindClosestDoor();
        if (door != null)
        {
            ConnectedDoor = door;
            ConnectedDoorRoom = door.GetComponent<Door>().room;
        }
    }

    private GameObject FindClosestDoor()
    {
        var sc = FindObjectOfType<StageController>().GetComponent<StageController>();
        int roomId = room.RoomId;
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Door");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            if (!sc.worldRooms.Contains(go.GetComponent<Door>().room))
            {
                continue;
            }
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && go.GetComponent<Door>().room.RoomId != roomId)
            {
                closest = go;
                distance = curDistance;
            }
        }

        if (distance < 2)
        {
            return closest;
        }
        else
        {
            return null;
        }
    }
}