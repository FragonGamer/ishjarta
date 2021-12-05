using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] int rangeModifier;
    [SerializeField] int luck;
    [SerializeField] int money;
    [SerializeField] int maxResistance;
    [SerializeField] int armor;
    public Inventory inventory;

    private void FixedUpdate()
    {
        Debug.Log(inventory.GetPassiveItems().Count);
    }
}
