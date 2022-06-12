using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class ActiveItem : Item, ISaveable
{
    public float cooldownTime;
    public float activeTime;

    public ActiveItemtype ItemType;



    public virtual void Activate(GameObject parent) {}

    public enum ActiveItemtype
    {
        speedBraclet
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
        this.ItemType = (ActiveItemtype)saveData.ItemType;
    }
    #endregion

}
