using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActiveItem : Item
{
    public float cooldownTime;
    public float activeTime;

    public ActiveItemtype ItemType;

    public virtual void Activate(GameObject parent) {}

    public enum ActiveItemtype
    {
        braclet
    }

    #region SaveSystem
    private bool isActiveItemInitialized = false;
    public void Init(ActiveItemData activeItemData)
    {
        if (!isActiveItemInitialized)
        {
            isActiveItemInitialized = true;
            base.Init(activeItemData);

            cooldownTime = activeItemData.cooldownTime;
            activeTime = activeItemData.activeTime;
            ItemType = (ActiveItemtype)activeItemData.activeItemType;
        }
    }
    #endregion SaveSystem
}
