using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PassiveItem : Item
{
    public PassivItemtype ItemType { get; set; }

    public enum PassivItemtype
    {
        flower
    }

    #region SaveSystem
    private bool isPassivItemInitialized = false;
    public void Init(PassivItemData passivItemData)
    {
        if (!isPassivItemInitialized)
        {
            isPassivItemInitialized = true;
            base.Init(passivItemData);

            ItemType = (PassivItemtype)passivItemData.passivItemType;
        }
    }
    #endregion SaveSystem
}
