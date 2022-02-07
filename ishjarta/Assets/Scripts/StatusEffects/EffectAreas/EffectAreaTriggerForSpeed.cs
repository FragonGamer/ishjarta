using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaTriggerForSpeed : MonoBehaviour
{
    [SerializeField] public int duration;
    [SerializeField] public float speedBoost;

    SpeedEffect speedEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.AddEffect(speedEffect);
        }
    }
    private void Awake()
    {
        speedEffect = SpeedEffect.CreateInstance(duration, speedBoost);
    }
}
