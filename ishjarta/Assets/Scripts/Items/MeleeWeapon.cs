using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeleeWeapon : Weapon
{
    [SerializeField] public float Range { get; set; } = 1.2f;
    [SerializeField] public float Width { get; set; } = 0.3f;

}
