using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeleeWeapon : Weapon
{
    [SerializeField] public float Range { get; set; }
    [SerializeField] public float Width { get; set; }
}
