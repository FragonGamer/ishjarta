using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private int rangeModifier;
    private int luck;
    private int money;
    private int maxResistance;
    private int armor;
    private Inventory inventory;

    public void Awake()
    {
        inventory = new Inventory();
    }
}
