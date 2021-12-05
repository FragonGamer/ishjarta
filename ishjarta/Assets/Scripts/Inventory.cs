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
        Coins = new UsableItem { Amount = 0, type = UsableItem.UItemtype.coin, MaxAmount = 999};
        Bombs = new UsableItem { Amount = 0, type = UsableItem.UItemtype.bomb, MaxAmount = 99};
        Keys = new UsableItem { Amount = 0, type = UsableItem.UItemtype.key, MaxAmount = 10};
        
    }

    public bool AddItem(Item item)
    {
        bool result;
        if (item = null)
        {
            return false;
        }

        if (item.GetType() == typeof(UsableItem))
        {
            AddUsableItem((UsableItem)item);
            result = true; 
        }
        else if (item.GetType() == typeof(PassiveItem))
        {
            AddPassiveItem((PassiveItem)item);
            result = true;
        }
        else if (item.GetType() == typeof(ActiveItem))
        {
            AddActiveItem((ActiveItem)item);
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }
    public void AddUsableItem(UsableItem item)
    {
        int itemAmount = 1;

        if (item.Amount > 1)
        {
            itemAmount = item.Amount;
        }

        switch (item.type)
        {
            case UsableItem.UItemtype.key:
                if (this.Keys.Amount <= this.Keys.MaxAmount)
                {
                    if (this.Keys.Amount + itemAmount > this.Keys.MaxAmount)
                    {
                        this.Keys.Amount = this.Keys.MaxAmount;
                    }
                    else
                    {
                        this.Keys.Amount += itemAmount;
                    }
                }
                break;
            case UsableItem.UItemtype.coin:
                if (this.Coins.Amount + itemAmount > this.Coins.MaxAmount)
                {
                    this.Coins.Amount = this.Coins.MaxAmount;
                }
                else
                {
                    this.Coins.Amount += itemAmount;
                }
                break;
            case UsableItem.UItemtype.bomb:
                if (this.Bombs.Amount + itemAmount > this.Bombs.MaxAmount)
                {
                    this.Bombs.Amount = this.Bombs.MaxAmount;
                }
                else
                {
                    this.Bombs.Amount += itemAmount;
                }
                break;
            default:
                break;
        }
    }
    public void AddActiveItem(ActiveItem item)
    {
        this.ActiveItem = item;
        DropItem(item);
    }
    public void AddPassiveItem(PassiveItem item)
    {
        PassiveItems.Add(item);
    }
    public void DropItem(Item item)
    {
    }
}
