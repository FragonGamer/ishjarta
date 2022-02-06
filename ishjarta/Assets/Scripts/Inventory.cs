using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Inventory :MonoBehaviour{ 
    MeleeWeapon MeleeWeapon { get; set; }
    RangedWeapon RangedWeapon { get; set; }
    public Weapon CurrentWeapon { get; set; }
    List<PassiveItem> PassiveItems { get; set; }
    ActiveItem ActiveItem { get; set; }
    UsableItem Coins { get; set; }
    UsableItem Bombs { get; set; }
    UsableItem Keys { get; set; }
    UsableItem Armor { get; set; }
    Player player { get; set; }

    #region Getters and Setters
    public MeleeWeapon GetMeleeWeapon()
    {
        return MeleeWeapon;
    }public RangedWeapon GetRangedWeapon()
    {
        return RangedWeapon;
    }
    public UsableItem GetArmor()
    {
        return this.Armor;
    }
    //Temp method
    public ActiveItem GetActiveItem()
    {
        return ActiveItem;
    }
    #endregion
    /// <summary>
    /// Our Constructor
    /// </summary>
    private void Awake()
    {
        PassiveItems = new List<PassiveItem>();
        MeleeWeapon = null;
        RangedWeapon = null;
        CurrentWeapon = null;
        ActiveItem = null;
        player = gameObject.GetComponent<Player>();
        Coins = ScriptableObject.CreateInstance(typeof(UsableItem)) as UsableItem;
        Bombs = ScriptableObject.CreateInstance(typeof(UsableItem)) as UsableItem;
        Keys = ScriptableObject.CreateInstance(typeof(UsableItem)) as UsableItem;
        Armor = ScriptableObject.CreateInstance(typeof(UsableItem)) as UsableItem;
        Coins.init(0, UsableItem.UItemtype.coin,999);
        Bombs.init( 0, UsableItem.UItemtype.bomb,99 );
        Keys.init(0, UsableItem.UItemtype.key, 10);
        Armor.init(0, UsableItem.UItemtype.armor, 999);
    }

    /// <summary>
    /// Adds Item 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool AddItem(Item item)
    {
        bool result = false;
        if (item != null)
        {
            

            if (item.GetType() == typeof(UsableItem))
            {
                AddUsableItem((UsableItem) item);
                result = true;
            }
            else if (item.GetType() == typeof(PassiveItem))
            {
                AddPassiveItem((PassiveItem) item);
                result = true;
            }
            else if (item.GetType() == typeof(ActiveItem) || item.GetType().IsSubclassOf(typeof(ActiveItem)))
            {
                Debug.Log("active item");
                AddActiveItem((ActiveItem) item);
                result = true;
            }
            else if (item.GetType().IsSubclassOf(typeof(Weapon)))
            {
                AddWeapon(item);
                result = true;
            }
            else
            {
                result = false;
            }

            PrintInventory();
        }

        return result;
        
    }

    public void ChangeWeapon()
    {   
        if (CurrentWeapon.GetType().IsSubclassOf(typeof(RangedWeapon)) && MeleeWeapon != null )
        {
            player.RemoveEffectRange(CurrentWeapon.OwnerEffects);
            CurrentWeapon = MeleeWeapon;
            player.AddEffectRange(CurrentWeapon.OwnerEffects);
        }
        else if (CurrentWeapon.GetType().IsSubclassOf(typeof(MeleeWeapon)) && RangedWeapon != null)
        {
            player.RemoveEffectRange(CurrentWeapon.OwnerEffects);
            CurrentWeapon = RangedWeapon;
            player.AddEffectRange(CurrentWeapon.OwnerEffects);
        }
        player.SetBaseDamage(CurrentWeapon.Damage);
        PrintInventory();
    }

    private void AddWeapon(Item item)
    {
        Type weapontype = item.GetType();
        if (weapontype.IsSubclassOf(typeof(MeleeWeapon)))
        {
            DropItem(MeleeWeapon);
            MeleeWeapon = (MeleeWeapon) item;

            player.RemoveEffectRange(CurrentWeapon.OwnerEffects);
            CurrentWeapon = null;
        }
        else if (weapontype.IsSubclassOf(typeof(RangedWeapon)))
        {
            DropItem(RangedWeapon);
            RangedWeapon = (RangedWeapon) item;

            player.RemoveEffectRange(CurrentWeapon.OwnerEffects);
            CurrentWeapon = null;
        }

        if (CurrentWeapon == null)
        {
            if (MeleeWeapon != null)
            {
                CurrentWeapon = MeleeWeapon;
            }
            else if (RangedWeapon != null)
            {
                CurrentWeapon = RangedWeapon;
            }
            if (CurrentWeapon != null)
            {
                player.AddEffectRange(CurrentWeapon.OwnerEffects);
                player.SetBaseDamage(CurrentWeapon.Damage);
            }
        }
        if (weapontype != CurrentWeapon.GetType())
        {
            ChangeWeapon();
        }

        
        Debug.Log(CurrentWeapon.name);
        CurrentWeapon = (Weapon)item;
    }

    /// <summary>
    /// Checks Usable Item Type and uses that to increase the amount
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
                if (this.Coins.Amount <= this.Coins.MaxAmount)
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
                if (this.Bombs.Amount <= this.Bombs.MaxAmount)
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
            case UsableItem.UItemtype.armor:
                if (this.Armor.Amount <= this.Armor.MaxAmount)
                {
                    if (this.Armor.Amount + 1 > this.Armor.MaxAmount)
                    {
                        this.Armor.Amount = this.Armor.MaxAmount;
                    }
                    else
                    {
                        this.Armor.Amount += 1;
                    }
                }

                player.CalcResistence();
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
        if(this.ActiveItem != null)
            DropItem(this.ActiveItem);

        this.ActiveItem = item;
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
        if (item != null)
        {
            Debug.Log(item.GetType());
            if (item.GetType() == typeof(PassiveItem))
            {
                PassiveItems.Remove((PassiveItem) item);
            }
            else if (item.GetType() == typeof(UsableItem))
            {
                DropUsableItem((UsableItem) item);
            }
            else if (item.GetType() == typeof(MeleeWeapon)|| item.GetType().IsSubclassOf(typeof(MeleeWeapon)))
            {
                SpawnItem(item);
                MeleeWeapon = null;

            }
            else if (item.GetType() == typeof(RangedWeapon)|| item.GetType().IsSubclassOf(typeof(RangedWeapon)))
            {
                SpawnItem(item);
                RangedWeapon = null;
            }
            else if (item.GetType() == typeof(ActiveItem) || item.GetType().IsSubclassOf(typeof(ActiveItem)))
            {
                SpawnItem(item);
                ActiveItem = null;
            }
        }
    }
    
    private void SpawnItem(Item item)
    {
        Vector2 playerPos = gameObject.transform.position;
        Type type = item.GetType();
        if (type == typeof(MeleeWeapon) || item.GetType().IsSubclassOf(typeof(MeleeWeapon)))
        {
            GameObject meleeWeapon = (GameObject)Resources.Load($"Prefabs/WeaponPrefab/Melee/{item.name}") as GameObject;
            Instantiate(meleeWeapon,playerPos + new Vector2(0,-0.25f),gameObject.transform.rotation);
        }
        else if (type == typeof(RangedWeapon) || item.GetType().IsSubclassOf(typeof(RangedWeapon)))
        { 
            GameObject rangedWeapon = (GameObject)Resources.Load($"Prefabs/WeaponPrefab/Ranged/{item.name}") as GameObject;
            Instantiate(rangedWeapon,playerPos + new Vector2(0,-0.25f),gameObject.transform.rotation);
        }
        else if (item.GetType() == typeof(ActiveItem) || item.GetType().IsSubclassOf(typeof(ActiveItem)))
        {
            GameObject activeItem = (GameObject)Resources.Load($"Prefabs/ActiveItemPrefabs/{item.name}") as GameObject;
            Instantiate(activeItem, playerPos + new Vector2(0,-0.25f),gameObject.transform.rotation);
        }
        
    }
    private void DropUsableItem(UsableItem item)
    {
        
            switch (item.type)
            {

                case UsableItem.UItemtype.bomb:
                    if (Bombs.Amount - item.Amount >= 0)
                    {
                        Bombs.Amount -= item.Amount;
                    }

                    break;
                case UsableItem.UItemtype.key:
                    if (Keys.Amount - item.Amount >= 0)
                    {
                        Keys.Amount -= item.Amount;
                    }

                    break;
                case UsableItem.UItemtype.coin:
                    if (Coins.Amount - item.Amount >= 0)
                    {
                        Coins.Amount -= item.Amount;
                    }

                    break;
            }
        

    }
    
    //Print to log console
    private void PrintInventory()
    {
        Debug.Log($"Armor : {this.Armor.Amount} : {player.GetResistence() * 100}%");
        Debug.Log($"Melee Weapon: {(MeleeWeapon != null ? MeleeWeapon.name : 'f')} | Ranged Weapon: {(RangedWeapon != null ? RangedWeapon.name : 'f')} | Active Weapon: {(CurrentWeapon != null ? CurrentWeapon.name : 'f')}");
    }

    public void UseActiveItem()
    {
        if (state == ActiveItemState.ready)
        {
            ActiveItem.Activate(gameObject);
            state = ActiveItemState.active;
            activeTime = ActiveItem.activeTime;
        }
    }


    // Active Item Usage

    float cooldownTime;
    float activeTime;

    enum ActiveItemState
    {
        ready,
        active,
        cooldown
    }
    ActiveItemState state = ActiveItemState.ready;
    void Update()
    {
        switch (state)
        {
            case ActiveItemState.active:
                if (activeTime > 0)
                    activeTime -= Time.deltaTime;
                else  
                {
                    state = ActiveItemState.cooldown;
                    cooldownTime = ActiveItem.cooldownTime;
                }
                break;
            case ActiveItemState.cooldown:
                if (cooldownTime > 0)
                    cooldownTime -= Time.deltaTime;
                else
                    state = ActiveItemState.ready;
                break;
        }
    }


}
