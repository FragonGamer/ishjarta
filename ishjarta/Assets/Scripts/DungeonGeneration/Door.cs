using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

/// <summary>
/// Represents a door of a room. Contains features for connecting to other doors, closing a door etc.
/// </summary>

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
    [SerializeField]public bool isLocked;
    public bool wasLocked;
    [SerializeField] public Tile lockedDoorTile;
    [SerializeField] public float range = 1f;
    private Player player;
    InputMaster inputMaster;
    public bool isInRange;

    public Room room;
    
private void Start() {
    player = FindObjectOfType<Player>();
    inputMaster.Player.UnlockDoor.performed += UnlockDoor;
}
private void Update() {
    
    if(room.IsCleared){
        CalculatRange();
        if(isLocked)
            wasLocked = true;

    }
}
private void OnEnable()
    {
        inputMaster.Enable();
    }
    private void OnDisable()
    {
        inputMaster.Disable();
    }
private void UnlockDoor(InputAction.CallbackContext context){
    if(isInRange&&isLocked){
        var inventory= player.inventory;
        if(inventory.GetKeys().Amount > 0){
            inventory.DropItem(new UsableItem(){type = UsableItem.UsableItemtype.key,Amount = 1});
            isLocked = false;
        }
    }
}
private void CalculatRange(){
    float distance = GetPlayerDistanceToDoor(this.player);
    if( distance <= range){
        isInRange = true;
    }
    else{
        isInRange=false;
    }
}
    public Room ConnectedDoorRoom;
    private void Awake()
    {
        inputMaster = new InputMaster();
        room = GetComponentInParent<Room>();
    }

    public float GetPlayerDistanceToDoor(Player player)
    {
        float distance = Vector3.Distance(this.transform.position, player.gameObject.transform.position);
        return distance;

    }
    public void TeleportPlayerToDoor()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = ConnectedDoor.transform.position;
        room.enteredDoor = null;
        room.isEntered = false;
        ConnectedDoorRoom.enteredDoor = ConnectedDoor.GetComponent<Door>();
        player.GetComponent<Player>().currentRoom = ConnectedDoorRoom;
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