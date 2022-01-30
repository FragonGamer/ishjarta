using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Sword : MeleeWeapon
{
    private void Awake()
    {
        Range = 1.2f;
        Width = 0.3f;
        attackRate = 1f;
    }

}
