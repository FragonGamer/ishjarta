using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : Item
{
    public enum UItemtype
    {
        key,
        coin,
        bomb
    }
    public UsableItem.UItemtype type;
    public int Amount { get; set; }
    

}
