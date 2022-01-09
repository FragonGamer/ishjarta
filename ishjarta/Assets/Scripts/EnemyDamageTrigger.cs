using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider is PolygonCollider2D)
        {
            Player p = collider.gameObject.GetComponent<Player>();

            ((Enemy)this.gameObject.GetComponent<Enemy>()).ReceiveDamage(p.DealingDamage);
        }
    }
}
