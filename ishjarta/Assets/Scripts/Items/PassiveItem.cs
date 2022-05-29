using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class PassiveItem : Item
{


    public PassivItemtype ItemType { get; set; }

    public enum PassivItemtype
    {
        speedFlower
    }


}
