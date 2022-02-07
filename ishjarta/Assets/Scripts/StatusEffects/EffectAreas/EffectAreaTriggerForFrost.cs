using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaTriggerForFrost : MonoBehaviour
{
    [SerializeField] public int duration;
    [SerializeField] public float speedDelay;

    FrostEffect frostEffect;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.AddEffect(frostEffect);
        }
    }
    private void Awake()
    {
        frostEffect = FrostEffect.CreateInstance(duration, speedDelay);
    }
}
