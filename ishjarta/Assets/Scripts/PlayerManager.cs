using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Player player;
    private Inventory playerInventory;
    [SerializeField] KeyCode itemPickupKeycode = KeyCode.E;

    private void Update()
    {
        if (Input.GetKeyDown(itemPickupKeycode))
        {

        }
    }
}
