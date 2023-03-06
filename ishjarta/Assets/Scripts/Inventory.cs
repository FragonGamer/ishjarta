using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.AddressableAssets;

public class Inventory : MonoBehaviour
{
    AudioSource audiosource;


    [field: SerializeField] public AudioClip arrowsound;

    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        instance = this;
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
        Coins.Init(0, UsableItem.UsableItemtype.coin, 999);
        Bombs.Init(0, UsableItem.UsableItemtype.bomb, 99);
        Keys.Init(0, UsableItem.UsableItemtype.key, 10);
        Armor.Init(0, UsableItem.UsableItemtype.armor, 999);
    }
    #endregion
    [field: SerializeField] MeleeWeapon MeleeWeapon { get; set; }
    [field: SerializeField] RangedWeapon RangedWeapon { get; set; }
    [field: SerializeField] public Weapon CurrentWeapon { get; set; }
    [field: SerializeField] List<PassiveItem> PassiveItems { get; set; }
    [field: SerializeField] List<PassiveItem> PeriodiclePassiveItems { get; set; }

    [field: SerializeField] ActiveItem ActiveItem { get; set; }
    [field: SerializeField] UsableItem Coins { get; set; }
    [field: SerializeField] UsableItem Bombs { get; set; }
    [field: SerializeField] UsableItem Keys { get; set; }
    [field: SerializeField] UsableItem Armor { get; set; }
    [field: SerializeField] Player player { get; set; }

    
    [SerializeField] private HUDManager hudManager;

    #region Getters and Setters
    public MeleeWeapon GetMeleeWeapon()
    {
        return MeleeWeapon;
    }
    public RangedWeapon GetRangedWeapon()
    {
        return RangedWeapon;
    }
    public UsableItem GetArmor()
    {
        return this.Armor;
    }
    public ActiveItem GetActiveItem()
    {
        return ActiveItem;
    }
    public UsableItem GetCoins()
    {
        return Coins;
    }
    public UsableItem GetKeys()
    {
        return Keys;
    }
    public UsableItem GetBombs()
    {
        return Bombs;
    }
    #endregion

    #region SaveSystem
    private bool isInventoryInitialized = false;
    public void Init(InventoryData inventoryData)
    {
        if (!isInventoryInitialized)
        {
            isInventoryInitialized = true;

            if (inventoryData.meleeWeapon != null)
            {
                var meleeWeaponItemBundle = Utils.LoadIRessourceLocations<GameObject>("MeleeWeapon");
                AddItem(Utils.LoadGameObjectByName(meleeWeaponItemBundle, inventoryData.meleeWeapon.itemName).GetComponent<MeleeWeapon>());
            }

            if (inventoryData.rangedWeapon != null)
            {
                var rangedWeaponItemBundle = Utils.LoadIRessourceLocations<GameObject>("rangedweapon");
                AddItem(Utils.LoadGameObjectByName(rangedWeaponItemBundle, inventoryData.rangedWeapon.itemName).GetComponent<MeleeWeapon>());
            }

            if (inventoryData.IsCurrentWeaponMelee && MeleeWeapon != null && CurrentWeapon is RangedWeapon)
                ChangeWeapon();

            inventoryData.passiveItems.ForEach(x =>
            {
                var passivItemBundle = Utils.LoadIRessourceLocations<GameObject>("PassiveItem");
                AddItem(Utils.LoadGameObjectByName(passivItemBundle, x.itemName).GetComponent<PassiveItem>());
            });

            if (inventoryData.activeItem != null)
            {
                var activeItemBundle = Utils.LoadIRessourceLocations<GameObject>("ActiveItem");
                AddItem(Utils.LoadGameObjectByName(activeItemBundle, inventoryData.activeItem.itemName).GetComponent<ActiveItem>());
            }

            Coins.Init(inventoryData.coins);
            Bombs.Init(inventoryData.bombs);
            Keys.Init(inventoryData.keys);
            Armor.Init(inventoryData.armor);

            //if(inventoryData.meleeWeapon != null)
            //    MeleeWeapon.Init(inventoryData.meleeWeapon);

            //if (inventoryData.rangedWeapon != null)
            //    RangedWeapon.Init(inventoryData.rangedWeapon);

            //CurrentWeapon = inventoryData.IsCurrentWeaponMelee ? MeleeWeapon : RangedWeapon;

            //PassiveItems.AddRange(inventoryData.passiveItems.Select(x =>
            //{
            //    var passivItem = ScriptableObject.CreateInstance<PassiveItem>();
            //    passivItem.Init(x);
            //    return passivItem;
            //}));
            //Debug.Log($"ActiveItem: {inventoryData.activeItem}");
            //ActiveItem.Init(inventoryData.activeItem);
            //Coins.Init(inventoryData.coins);
            //Bombs.Init(inventoryData.bombs);
            //Keys.Init(inventoryData.keys);
            //Armor.Init(inventoryData.armor);
        }
    }
    #endregion SaveSystem
    private void Start()
    {
        hudManager = HUDManager.instance;
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
                AddUsableItem((UsableItem)item);
                result = true;
            }
            else if (item.GetType() == typeof(PassiveItem) || item.GetType().IsSubclassOf(typeof(PassiveItem)))
            {
                AddPassiveItem((PassiveItem)item);
                result = true;
            }
            else if (item.GetType() == typeof(ActiveItem) || item.GetType().IsSubclassOf(typeof(ActiveItem)))
            {
                Debug.Log("active item");
                AddActiveItem((ActiveItem)item);
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
            if (hudManager != null)
                hudManager.UpdateAllSpritesAndText();
            PrintInventory();
        }

        
        return result;

    }

    public void ChangeWeapon()
    {        
        if (CurrentWeapon.GetType() == typeof(RangedWeapon) && MeleeWeapon != null)
        {
            player.RemoveEffectRange(CurrentWeapon.OwnerEffects);
            CurrentWeapon = MeleeWeapon;
            player.AddEffectRange(CurrentWeapon.OwnerEffects);
        }
        else if (CurrentWeapon.GetType() == typeof(MeleeWeapon) && RangedWeapon != null)
        {
            player.RemoveEffectRange(CurrentWeapon.OwnerEffects);
            CurrentWeapon = RangedWeapon;
            player.AddEffectRange(CurrentWeapon.OwnerEffects);
        }
        if (hudManager != null)
            hudManager.UpdateWeaponSprites();
        player.SetBaseDamage(CurrentWeapon.Damage);
        PrintInventory();
    }

    private void AddWeapon(Item item)
    {
        
        Type weapontype = item.GetType();
        if (weapontype.IsSubclassOf(typeof(MeleeWeapon)) || weapontype == typeof(MeleeWeapon))
        {
            if (MeleeWeapon != null)
            {
                DropItem(MeleeWeapon);
                //player.RemoveEffectRange(CurrentWeapon.OwnerEffects);
                CurrentWeapon = null;
            }
            MeleeWeapon = (MeleeWeapon)item;

        }
        else if (weapontype.IsSubclassOf(typeof(RangedWeapon)) || weapontype == typeof(RangedWeapon))
        {
            if (RangedWeapon != null)
            {
                DropItem(RangedWeapon);
                //player.RemoveEffectRange(CurrentWeapon.OwnerEffects);
                CurrentWeapon = null;
            }
            RangedWeapon = (RangedWeapon)item;

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
        int itemAmount = 0;

        if (item.Amount > 0)
        {
            itemAmount = item.Amount;
        }

        switch (item.type)
        {
            case UsableItem.UsableItemtype.key:
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
            case UsableItem.UsableItemtype.coin:
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
            case UsableItem.UsableItemtype.bomb:
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
            case UsableItem.UsableItemtype.armor:
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
        if (this.ActiveItem != null)
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
        item.triggerEffect();
        if(item.isPeriodicle)
            PeriodiclePassiveItems.Add(item);
        
        //player.AddEffectRange(item.OwnerEffects);
    }

    public void RemovePeriodiclePassiveItem(PassiveItem item)
    {
        PeriodiclePassiveItems.Remove(item);
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
                ((PassiveItem)item).removeEffect();
                if(((PassiveItem)item).isPeriodicle)
                    PeriodiclePassiveItems.Remove((PassiveItem)item);
                if (PassiveItems.Remove((PassiveItem)item))
                    player.RemoveEffectRange(item.OwnerEffects);
                    
                

            }
            else if (item.GetType() == typeof(UsableItem))
            {
                DropUsableItem((UsableItem)item);
            }
            else if (item.GetType() == typeof(MeleeWeapon) || item.GetType().IsSubclassOf(typeof(MeleeWeapon)))
            {
                SpawnItem(item);
                MeleeWeapon = null;

            }
            else if (item.GetType() == typeof(RangedWeapon) || item.GetType().IsSubclassOf(typeof(RangedWeapon)))
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
        var item_type = item.GetType();
        GameObject go = null;  
         var assets = new List<GameObject>();
         var gos = new List<GameObject>();
        if(item_type.IsSubclassOf(typeof(Weapon))){
           var reference = Utils.LoadIRessourceLocations<GameObject>(new string[] { "Weapon" });
           assets = Utils.LoadMultipleObjects<GameObject>(reference).ToList();
        }
        else{
           var reference = Utils.LoadIRessourceLocations<GameObject>(new string[] { "Item" });
           assets = Utils.LoadMultipleObjects<GameObject>(reference).ToList();
        }
        
        Vector2 playerPos = gameObject.transform.position;
        Type type = item.GetType();
        if (type == typeof(MeleeWeapon) || item.GetType().IsSubclassOf(typeof(MeleeWeapon)))
        {
            GameObject meleeWeapon = assets.Where(i => i.GetComponent<ItemManager>().GetItem().ItemName == item.ItemName).FirstOrDefault();
            go = Instantiate(meleeWeapon, playerPos + new Vector2(0, -0.25f), gameObject.transform.rotation);
        }
        else if (type == typeof(RangedWeapon) || item.GetType().IsSubclassOf(typeof(RangedWeapon)))
        {
            GameObject rangedWeapon = assets.Where(i => i.GetComponent<ItemManager>().GetItem().ItemName == item.ItemName).FirstOrDefault();
            go = Instantiate(rangedWeapon, playerPos + new Vector2(0, -0.25f), gameObject.transform.rotation);
        }
        else if (item.GetType() == typeof(ActiveItem) || item.GetType().IsSubclassOf(typeof(ActiveItem)))
        {
           
            GameObject activeItem = assets.Where(i => i.GetComponent<ItemManager>().GetItem().ItemName == item.ItemName).FirstOrDefault();
            if (activeItem != null)
            // GameObject activeItem = (GameObject)Resources.Load($"Prefabs/ActiveItemPrefabs/{item.name}") as GameObject;
                go = Instantiate(activeItem, playerPos + new Vector2(0, -0.25f), gameObject.transform.rotation);
        }
        go.transform.SetParent(player.currentRoom.gameObject.transform);

    }
    private void DropUsableItem(UsableItem item)
    {

        switch (item.type)
        {

            case UsableItem.UsableItemtype.bomb:
                if (Bombs.Amount - item.Amount >= 0)
                {
                    Bombs.Amount -= item.Amount;
                }

                break;
            case UsableItem.UsableItemtype.key:
                if (Keys.Amount - item.Amount >= 0)
                {
                    Keys.Amount -= item.Amount;
                }

                break;
            case UsableItem.UsableItemtype.coin:
                if (Coins.Amount - item.Amount >= 0)
                {
                    Coins.Amount -= item.Amount;
                }

                break;
            case UsableItem.UsableItemtype.armor:
                if (Armor.Amount - item.Amount >= 0)
                {
                    Armor.Amount -= item.Amount;
                }
                player.CalcResistence();

                break;
        }


    }

    //Print to log console
    private void PrintInventory()
    {
        Debug.Log($"Armor : {this.Armor.Amount} : {player.GetResistence() * 100}%");
        Debug.Log($"Coins : {this.Coins.Amount} ");
        Debug.Log($"Melee Weapon: {(MeleeWeapon != null ? MeleeWeapon.name : 'f')} | Ranged Weapon: {(RangedWeapon != null ? RangedWeapon.name : 'f')} | Active Weapon: {(CurrentWeapon != null ? CurrentWeapon.name : 'f')}");
    }

    public void UseActiveItem()
    {
        if (ActiveItem != null&&state == ActiveItemState.ready)
        {
            ActiveItem.Activate(gameObject);
            state = ActiveItemState.active;
            activeTime = ActiveItem.activeTime;
        }
    }

    public ActiveItemState GetState(){
        return state;
    }
    // Active Item Usage

    float cooldownTime;
    float activeTime;

    public enum ActiveItemState
    {
        ready,
        active,
        cooldown
    }
    ActiveItemState state = ActiveItemState.ready;

    void ActivateAllperiodicPassivItems()
    {
        if (PeriodiclePassiveItems.Count != 0)
        {
            for (int i = PeriodiclePassiveItems.Count - 1; i >= 0; i--)
            {
                PeriodiclePassiveItems[i].triggerEffect();
            }
        }
    }
    public UsableItem GetUsableItem(UsableItem.UsableItemtype itemtype){
        var p = Utils.LoadIRessourceLocations<ScriptableObject>(new string[] { "ScriptableObject", "UsableItem" });
        var name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(itemtype.ToString().ToLower());
        var item = Utils.LoadItemByName<UsableItem>(p, name);
        
        return item;
    }
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
        ActivateAllperiodicPassivItems();
    }

    


}
