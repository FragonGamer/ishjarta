using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider is PolygonCollider2D)
        {
            Player player = collider.gameObject.GetComponent<Player>();
            Enemy enemy = this.gameObject.GetComponent<Enemy>();

            enemy.ReceiveDamage(player.DealingDamage);
            enemy.AddEffectRange(player.GetCurrentEffectOfMeleeWeapon);
        }
    }
}
