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
    private TextMesh Name;
    private TextMesh Description;
    private TextMesh FullDescription;
    private bool showFullDescription;
    Vector3 NamePosition;
    Vector3 FullDescriptionPosition;

    Vector3 DescriptionPosition;
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
        var assets = Utils.LoadIRessourceLocations<GameObject>(new string[] { "TextObject" }).First();
        Name = Utils.InstantiateGameObject(assets, this.gameObject.transform.position, new Quaternion(0, 0, 0, 0)).GetComponentInChildren<TextMesh>();
        Description = Utils.InstantiateGameObject(assets, this.gameObject.transform.position, new Quaternion(0, 0, 0, 0)).GetComponentInChildren<TextMesh>();
        FullDescription = Utils.InstantiateGameObject(assets, this.gameObject.transform.position, new Quaternion(0, 0, 0, 0)).GetComponentInChildren<TextMesh>();

        Name.transform.SetParent(this.gameObject.transform, false);
        Description.transform.SetParent(this.gameObject.transform, false);
        FullDescription.transform.SetParent(this.gameObject.transform, false);
        
        NamePosition = new Vector3(.2f,0,0);
        DescriptionPosition = new Vector3(.2f,-.15f,0);
        FullDescriptionPosition =  new Vector3(.2f,-.15f,0);
        
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
            float horizontalPosition =  gameObject.transform.position.x - player.transform.position.x;
            if(horizontalPosition < 0){
                Vector3 pos = NamePosition;
                pos.x  = pos.x *-1;
                Name.transform.position = gameObject.transform.position + pos ;
                Name.anchor = TextAnchor.MiddleRight;
                pos = DescriptionPosition;
                pos.x  = pos.x *-1;
                Description.transform.position = gameObject.transform.position  + pos;
                Description.anchor = TextAnchor.MiddleRight;
                pos = FullDescriptionPosition;
                pos.x  = pos.x *-1;
                FullDescription.transform.position  = gameObject.transform.position + pos;
                FullDescription.anchor = TextAnchor.MiddleRight;
            }
            else{
                Name.transform.position = gameObject.transform.position + NamePosition;
                
               Name.anchor = TextAnchor.MiddleLeft;
                Description.transform.position = gameObject.transform.position + DescriptionPosition;
                Description.anchor = TextAnchor.MiddleLeft;
                FullDescription.transform.position  = gameObject.transform.position + FullDescriptionPosition;
                FullDescription.anchor = TextAnchor.MiddleLeft;
            }
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
