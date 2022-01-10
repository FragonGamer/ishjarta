using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeleeWeapon : Weapon
{
    [SerializeField] public float Range { get; set; }
    [SerializeField] public float Width { get; set; }

    void Start()
    {
        Range = 1.2f;
        Width = 0.3f;
    }
}
