using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Player player;
    [SerializeField] KeyCode itemPickupKeycode = KeyCode.E;

    private bool isInRange;

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(itemPickupKeycode))
        {
            if (player.inventory.AddItem(item) == true)
            {
                GameObject.Destroy(this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isInRange = true;    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
    }


}
