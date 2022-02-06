using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaTrigger : MonoBehaviour
{
    SpeedEffect speedEffect;
    PoisiningEffect poisiningEffect;
    IncinerationEffect incinerationEffect;
    RegenerationEffect regenerationEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.AddEffect(regenerationEffect);
        }
    }
    private void Awake()
    {
        speedEffect = SpeedEffect.CreateInstance(5, 5);
        poisiningEffect = PoisiningEffect.CreateInstance(5, 10);
        incinerationEffect = IncinerationEffect.CreateInstance(5, 5, 5);
        regenerationEffect = RegenerationEffect.CreateInstance(true, 5);
    }
}
