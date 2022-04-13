using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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

            TeleportPlayerToDoor();
            var stagecontroller = FindObjectOfType<StageController>();
            if (!stagecontroller.TestGeneration)
            {
                ConnectedDoor.GetComponentInParent<Room>().ToggleRoomState();
                room.ToggleRoomState();
            }

        }
    }

    public void AttachClosestDoor()
    {
        var door = FindClosestDoor();
        if (door != null)
        {
            ConnectedDoor = door;
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

        if (distance < 7)
        {
            return closest;
        }
        else
        {
            return null;
        }
    }
}