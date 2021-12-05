using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private MeleeWeapon MeleeWeapon { get; set; }
    private RangedWeapon RangedWeapon { get; set; }
    private List<PassiveItem> PassiveItems { get; set; }
    private ActiveItem ActiveItem { get; set; }
    private UsableItem Coins { get; set; }
    private UsableItem Bombs { get; set; }
    private UsableItem Keys { get; set; }


    public Inventory()
    {
        MeleeWeapon = null;
        RangedWeapon = null;
        PassiveItems = new List<PassiveItem>();
        ActiveItem = null;
        Coins = new UsableItem { Amount = 0, type = UsableItem.UItemtype.coin};
        Bombs = new UsableItem { Amount = 0, type = UsableItem.UItemtype.bomb};
        Keys = new UsableItem { Amount = 0, type = UsableItem.UItemtype.key};
        
    }


}
