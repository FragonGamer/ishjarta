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
    public Room room;
    public Room ConnectedDoorRoom;


    private void Awake()
    {
        room = GetComponentInParent<Room>();
    }


    public void TeleportPlayerToDoor()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(ConnectedDoor.GetComponentInParent<Room>().RoomId);
        switch (ConnectedDoor.GetComponent<Door>().direction)
        {
            case Direction.West:
                player.transform.position = ConnectedDoor.transform.position + new Vector3(1, 0, 0);
                break;
            case Direction.East:
                player.transform.position = ConnectedDoor.transform.position + new Vector3(-1, 0, 0);
                break;
            case Direction.North:
                player.transform.position = ConnectedDoor.transform.position + new Vector3(0, -1, 0);
                break;
            case Direction.South:
                player.transform.position = ConnectedDoor.transform.position + new Vector3(0, 1, 0);
                break;
            default:
                return;
        }
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
            foreach (var position in tilemap.cellBounds.allPositionsWithin)
            {
                if (!tilemap.HasTile(position))
                {
                    continue;
                }

                Debug.Log(position.x + "/" + position.y);
            }

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