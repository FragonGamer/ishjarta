using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public MeleeWeapon MeleeWeapon { get; set; }
    public RangedWeapon RangedWeapon { get; set; }
    public List<Item> PassiveItems { get; set; }
    public Item ActiveItem { get; set; }

    
    public Inventory()
    {
        MeleeWeapon = null;
        RangedWeapon = null;
        PassiveItems = new List<Item>();
        ActiveItem = null;
    }

}
