using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaTriggerForSpeeds : MonoBehaviour
{
    [SerializeField] public int duration;
    [SerializeField] public float speedBoost;

    SpeedEffect[] speedEffects;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.AddEffectRange(speedEffects);
        }
    }
    private void Awake()
    {
        speedEffects = new SpeedEffect[]
        {
            SpeedEffect.CreateInstance(duration, speedBoost),
            SpeedEffect.CreateInstance(duration, speedBoost),
            SpeedEffect.CreateInstance(duration, speedBoost),
            SpeedEffect.CreateInstance(duration, speedBoost),
            SpeedEffect.CreateInstance(duration, speedBoost),
            SpeedEffect.CreateInstance(duration, speedBoost),
            SpeedEffect.CreateInstance(duration, speedBoost),
            SpeedEffect.CreateInstance(duration, speedBoost),
            SpeedEffect.CreateInstance(duration, speedBoost),
            SpeedEffect.CreateInstance(duration, speedBoost),
        };
    }
}
