using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaTrigger : MonoBehaviour
{
    FrostEffect frostEffect;
    PoisoningEffect poisiningEffect;
    IncinerationEffect incinerationEffect;
    RegenerationEffect regenerationEffect;
    SpeedEffect speedEffect;
    StrengthEffect strengthEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.AddEffect(poisiningEffect);
        }
    }
    private void Awake()
    {
        frostEffect = FrostEffect.CreateInstance(10, 0.5f);
        speedEffect = SpeedEffect.CreateInstance(5, 5);
        poisiningEffect = PoisoningEffect.CreateInstance(5, 5);
        incinerationEffect = IncinerationEffect.CreateInstance(5, 5, 10);
        regenerationEffect = RegenerationEffect.CreateInstance(5, 5);
        strengthEffect = StrengthEffect.CreateInstance(5, 2);
    }
}
