using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class ActiveItem : Item
{
    public float cooldownTime;
    public float activeTime;

    public ActiveItemtype ItemType;



    public virtual void Activate(GameObject parent) {}

    public enum ActiveItemtype
    {
        speedBraclet
    }


}
