using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Player player;
    private void Awake()
    {
        player = (Player)GameObject.FindWithTag("Player").GetComponent(typeof(Player));
    }

    private void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            player.Attack(Input.mousePosition);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Dropped Item");
            if (player.inventory.GetActiveItem() != null)
            {
                ActiveItem activeItem = player.inventory.GetActiveItem();
                player.inventory.DropItem(activeItem);
            }
        }
    }

}
