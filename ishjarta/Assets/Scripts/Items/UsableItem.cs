using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class UsableItem : Item, ISaveable
{
    public enum UsableItemtype
    {
        key,
        coin,
        bomb,
        armor
    }
    public UsableItem.UsableItemtype type;



    public int Amount {get;set;}
    public int MaxAmount { get; set; }

    public void Init( int a, UsableItemtype Itype, int ma)
    {
        type = Itype;
        MaxAmount = ma;
        Amount = a;
    }


    #region SaveSystem
    public override object SaveState()
    {
        return new SaveData()
        {
            ItemName = this.ItemName,
            ItemType = (int)this.type
        };
    }

    public override void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.ItemName = saveData.ItemName;
        this.type = (UsableItemtype)saveData.ItemType;
    }
    #endregion
}
