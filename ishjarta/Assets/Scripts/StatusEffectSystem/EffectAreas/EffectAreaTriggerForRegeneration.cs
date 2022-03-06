using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaTriggerForRegeneration : MonoBehaviour
{
    [SerializeField] public int duration;
    [SerializeField] public int regeneration;

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
        regenerationEffect = RegenerationEffect.CreateInstance(duration, regeneration);
    }
}

