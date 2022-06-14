using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemManager : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Player player;
    [SerializeField] InputMaster inputMaster;
    [SerializeField] public bool isNearest;
    public bool isInRange;
    private TextMesh Name;
    private TextMesh Description;
    private TextMesh FullDescription;
    private bool showFullDescription;

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
        var assets = Utils.loadAssetPack("special");
        var t = Utils.loadAssetFromAssetPack(assets, "TextObject");
        Name = Instantiate(t,this.gameObject.transform.position,new Quaternion(0,0,0,0)).GetComponentInChildren<TextMesh>();
        Description = Instantiate(t,this.gameObject.transform.position,new Quaternion(0,0,0,0)).GetComponentInChildren<TextMesh>();
        FullDescription = Instantiate(t,this.gameObject.transform.position,new Quaternion(0,0,0,0)).GetComponentInChildren<TextMesh>();
        assets.Unload(false);
        
        Name.transform.parent = gameObject.transform;
        Description.transform.parent = gameObject.transform;
        FullDescription.transform.parent = gameObject.transform;
        
        
        
        Name.transform.position = transform.position + new Vector3(.2f,0,0);
        Description.transform.position = Name.gameObject.transform.position + new Vector3(0,-.15f,0);
        FullDescription.transform.position = Name.gameObject.transform.position + new Vector3(0,-.15f,0);
        
        Name.text = item.ItemName;
        Description.text = item.description;
        FullDescription.text = item.fullDescription;
        
        Name.gameObject.SetActive(false);
        Description.gameObject.SetActive(false);
        FullDescription.gameObject.SetActive(false);
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
            Name.gameObject.SetActive(true);
            Description.gameObject.SetActive(true);
        }
        else if (!isNearest)
        {
            Name.gameObject.SetActive(false);
            Description.gameObject.SetActive(false);
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
