using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public abstract class PassiveItem : Item, PassivItemInterface
{
    public PassivItemtype ItemType { get; set; }
    [SerializeField] public bool isPeriodicle = false;
    public enum PassivItemtype
    {
        speedFlower
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


    public abstract void triggerEffect();
    public abstract void removeEffect();

}
