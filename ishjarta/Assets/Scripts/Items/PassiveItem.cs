using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class PassiveItem : Item , ISaveable
{


    public PassivItemtype ItemType { get; set; }

    public enum PassivItemtype
    {
        speedFlower
    }

    #region SaveSystem
    public override object SaveState()
    {
        return new SaveData()
        {
            ItemName = this.ItemName,
            ItemType = (int)this.ItemType
        };
    }

    public override void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.ItemName = saveData.ItemName;
        this.ItemType = (PassivItemtype)saveData.ItemType;
    }
    #endregion


}
