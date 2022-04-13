using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider is PolygonCollider2D)
        {
            //Player player = collider.gameObject.GetComponent<Player>();
            //Enemy enemy = this.gameObject.GetComponent<Enemy>();

            //enemy.ReceiveDamage(player.DealingDamage);
            //enemy.AddEffectRange(player.GetCurrentEffects);

            if (this.gameObject.tag == "Enemy")
            {
                Player player = FindObjectOfType<Player>();
                Enemy enemy = this.gameObject.GetComponent<Enemy>();

                enemy.ReceiveDamage(player.DealingDamage);
                enemy.AddEffectRange(player.GetCurrentEffects);
            }
            else if (this.gameObject.tag == "Player")
            {
                Player player = this.gameObject.GetComponent<Player>();
                Enemy enemy = collider.gameObject.GetComponent<Enemy>();

                player.ReceiveDamage(enemy.DealingDamage);
                player.AddEffect(enemy.EmitEffect);
            }
        }
    }
}
