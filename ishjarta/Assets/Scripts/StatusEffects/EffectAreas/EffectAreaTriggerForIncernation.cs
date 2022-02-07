using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaTriggerForIncernation : MonoBehaviour
{
    [SerializeField] public int duration;
    [SerializeField] public float resistanceReduction;
    [SerializeField] public int incinerationDamage;

    IncinerationEffect incinerationEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.AddEffect(incinerationEffect);
        }
    }
    private void Awake()
    {
        incinerationEffect = IncinerationEffect.CreateInstance(duration, resistanceReduction, incinerationDamage);
    }
}
