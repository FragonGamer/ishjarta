using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory :MonoBehaviour{ 
    [SerializeField] MeleeWeapon MeleeWeapon { get; set; }
    [SerializeField] RangedWeapon RangedWeapon { get; set; }
    [SerializeField] List<PassiveItem> PassiveItems { get; set; }
    [SerializeField] ActiveItem ActiveItem { get; set; }
    [SerializeField] UsableItem Coins { get; set; }
    [SerializeField] UsableItem Bombs { get; set; }
    [SerializeField] UsableItem Keys { get; set; }

    /// <summary>
    /// Our Constructor
    /// </summary>
    private void Awake()
    {
        PassiveItems = new List<PassiveItem>();
        MeleeWeapon = null;
        RangedWeapon = null;
        ActiveItem = null;
        Coins = ScriptableObject.CreateInstance(typeof(UsableItem)) as UsableItem;
        Bombs = ScriptableObject.CreateInstance(typeof(UsableItem)) as UsableItem;
        Keys = ScriptableObject.CreateInstance(typeof(UsableItem)) as UsableItem;
        Coins.init(0, UsableItem.UItemtype.coin,999);
        Bombs.init( 0, UsableItem.UItemtype.bomb,99 );
        Keys.init(0, UsableItem.UItemtype.key, 10);
    }

    /// <summary>
    /// Adds Item 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool AddItem(Item item)
    {
        bool result;

        if (item.GetType() == typeof(UsableItem))
        {
            Debug.Log("Is Usable Item");
            AddUsableItem((UsableItem)item);
            result = true; 
        }
        else if (item.GetType() == typeof(PassiveItem))
        {
            Debug.Log("Is Passive Item");
            AddPassiveItem((PassiveItem)item);
            result = true;
        }
        else if (item.GetType() == typeof(ActiveItem))
        {
            Debug.Log("Is Active Item");
            AddActiveItem((ActiveItem)item);
            result = true;
        }
        else
        {
            Debug.Log("Is None");
            result = false;
        }
        return result;
    }

    /// <summary>
    /// Checks Usable Item Type and uses that to increase the amoint
    /// </summary>
    /// <param name="item"></param>
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
                if (this.Coins.Amount <= this.Keys.MaxAmount)
                {
                    if (this.Coins.Amount + itemAmount > this.Coins.MaxAmount)
                    {
                        this.Coins.Amount = this.Coins.MaxAmount;
                    }
                    else
                    {
                        this.Coins.Amount += itemAmount;
                    }
                }
                break;
            case UsableItem.UItemtype.bomb:
                if (this.Bombs.Amount <= this.Keys.MaxAmount)
                {
                    if (this.Bombs.Amount + itemAmount > this.Bombs.MaxAmount)
                    {
                        this.Bombs.Amount = this.Bombs.MaxAmount;
                    }
                    else
                    {
                        this.Bombs.Amount += itemAmount;
                    }
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Adds Active Item and drops other Item
    /// </summary>
    /// <param name="item"></param>
    public void AddActiveItem(ActiveItem item)
    {
        this.ActiveItem = item;
        DropItem(item);
    }
    /// <summary>
    /// Adds passive Item
    /// </summary>
    /// <param name="item"></param>
    public void AddPassiveItem(PassiveItem item)
    {
        PassiveItems.Add(item);
    }

    /// <summary>
    /// returns Passive Items
    /// </summary>
    /// <returns></returns>
    public List<PassiveItem> GetPassiveItems()
    {
        return PassiveItems;
    }
    /// <summary>
    /// Drops Item from Inventory and spawns it back into the world
    /// </summary>
    /// <param name="item"></param>
    public void DropItem(Item item)
    {
    }
}
