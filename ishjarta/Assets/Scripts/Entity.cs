using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    //Health
    private int currentHealth;
    private int maxHealth;
    private int baseHealth;
    private float healthModifier;
    //Armor
    private int resistance;
    //Movement
    private int movementSpeed;
    //Damage
    private int baseDamage;
    private float damageModifier;
    //AttackRate
    private int attackRate;
    //Range
    private int range;
}
