using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UsableItem : Item
{
    public enum UsableItemtype
    {
        key,
        coin,
        bomb,
        armor
    }
    public UsableItem.UsableItemtype type;
    [SerializeField]public int Amount {get;set;} = 1;
    public int MaxAmount { get; set; }

    public void Init( int a, UsableItemtype Itype, int ma)
    {
        type = Itype;
        MaxAmount = ma;
        Amount = a;
    }

    #region SaveSystem
    private bool isUsableItemInitialized = false;
    public void Init(UsableItemData usableItemData)
    {
        if (!isUsableItemInitialized)
        {
            isUsableItemInitialized = true;
            base.Init(usableItemData);

            Amount = usableItemData.amount;
            MaxAmount = usableItemData.maxAmount;
            type = (UsableItemtype)usableItemData.usabelItemType;
        }
    }
    #endregion SaveSystem
}
