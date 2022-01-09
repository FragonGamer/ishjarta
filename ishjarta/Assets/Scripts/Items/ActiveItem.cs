using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActiveItem : Item
{
    public float cooldownTime;
    public float activeTime;

    public virtual void Activate(GameObject parent) {}
}
