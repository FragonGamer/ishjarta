using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaTriggerForPoisining : MonoBehaviour
{
    [SerializeField] public float duration;
    [SerializeField] public int poisonDamage;

    PoisoningEffect poisiningEffect;

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
        poisiningEffect = PoisoningEffect.CreateInstance(duration, poisonDamage);
    }
}
