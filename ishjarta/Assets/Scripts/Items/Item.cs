using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string ItemName;
    [SerializeField] Sprite Icon;
    protected int dropChanceMax,dropChanceMin;

}
