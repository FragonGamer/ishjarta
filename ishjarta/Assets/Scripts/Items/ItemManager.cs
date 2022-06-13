using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemManager : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Player player;
    [SerializeField] InputMaster inputMaster;
    public bool isInRange;

    public Item GetItem()
    {
        return item;
    }
    public void SetItem(Item item)
    {
        this.item = item;
    }

    private void Awake()
    {
        inputMaster = new InputMaster();
    }
    private void OnEnable()
    {
        inputMaster.Enable();
    }
    private void OnDisable()
    {
        inputMaster.Disable();
    }

    private void Start()
    {
        player = (Player)GameObject.FindWithTag("Player").GetComponent(typeof(Player));
        inputMaster.Player.PickUpItem.performed += PickUpItem;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isInRange = false;
        }
    }

    public void PickUpItem(InputAction.CallbackContext context)
    {
        if (isInRange)
        {
            if (player.currentRoom.CompareTag("Merchant"))
            {
                if (!player.currentRoom.GetComponent<Merchant>().BuyItem(player,item))
                {
                    Debug.Log("Not enough money!");
                    return;
                }
            }
            if (player.inventory.AddItem(item) == true)
            {
                Debug.Log("Pick ups item");
                GameObject.Destroy(gameObject);
            }
        }
    }
}
