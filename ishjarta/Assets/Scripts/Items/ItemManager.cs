using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Player player;
    [SerializeField] KeyCode itemPickupKeycode = KeyCode.E;

    public bool isInRange;

    private void Awake()
    {
        player = (Player)GameObject.FindWithTag("Player").GetComponent(typeof(Player));
    }

    /// <summary>
    /// Picks up Item into Inventory if player is in range
    /// </summary>
    private void Update()
    {
        if (isInRange && Input.GetKeyDown(itemPickupKeycode))
        {
            Debug.Log("is in range");
            if (player.inventory.AddItem(item) == true)
            {
                Debug.Log("Pick ups item");
                GameObject.Destroy(gameObject);
            }
        }
        
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


}
