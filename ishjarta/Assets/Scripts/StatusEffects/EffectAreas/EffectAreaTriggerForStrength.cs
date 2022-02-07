using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaTriggerForStrength : MonoBehaviour
{
    [SerializeField] public int duration;
    [SerializeField] public float strengthBoost;

    StrengthEffect strengthEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.AddEffect(strengthEffect);
        }
    }
    private void Awake()
    {
        strengthEffect = StrengthEffect.CreateInstance(duration, strengthBoost);
    }
}
