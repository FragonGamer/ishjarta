using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "passiv items/AddArmor")]
public class AddArmor : PassiveItem
{
    [SerializeField] public int Amount;

    public override async void triggerEffect()
    {
        await new Task(() =>
        {
            var p = Utils.LoadIRessourceLocations<ScriptableObject>(new string[] { "ScriptableObject", "UsableItem" });
            var armor = Utils.LoadItemByName<UsableItem>(p, "Armor");
            var inv = Inventory.instance;
            armor.Amount = Amount;
            inv.AddUsableItem(armor);


            Inventory.instance.RemovePeriodiclePassiveItem(this);
        });
    }

    public override void removeEffect()
    {
        var p = Utils.LoadIRessourceLocations<ScriptableObject>(new string[] { "ScriptableObject", "UsableItem" });
        var armor = Utils.LoadItemByName<UsableItem>(p, "Armor");
        var inv = Inventory.instance;
        armor.Amount = Amount;
        Inventory.instance.DropItem(armor);
    }
}