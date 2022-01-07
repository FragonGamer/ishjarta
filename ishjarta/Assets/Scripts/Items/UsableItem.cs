using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UsableItem : Item
{
    public enum UItemtype
    {
        key,
        coin,
        bomb,
        armor
    }
    public UsableItem.UItemtype type;
    public int Amount { get; set; }
    public int MaxAmount { get; set; }

    public void init( int a, UItemtype Itype, int ma)
    {
        type = Itype;
        MaxAmount = ma;
        Amount = a;
    }
}
