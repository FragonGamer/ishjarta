using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

public class ItemManager : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Player player;
    [SerializeField] InputMaster inputMaster;
    [SerializeField] public bool isNearest;
    public bool isInRange;
    
    public bool showFullDescription;
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
        var assets = Utils.LoadIRessourceLocations<GameObject>(new string[] { "Objects" }, Addressables.MergeMode.Intersection);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
                isInRange = true;
        }
    }

    private void Update()
    {
        
        if (isNearest && isInRange)
        {
            
            GetComponentInChildren<SpriteRenderer>().color = Color.gray;
            

        }
        else if (!isNearest)if (!isNearest)
        {
           
            GetComponentInChildren<SpriteRenderer>().color = Color.white;

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
        if (isInRange && isNearest)
        {
            if (player.currentRoom.name.Contains("MerchantRoom"))
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
