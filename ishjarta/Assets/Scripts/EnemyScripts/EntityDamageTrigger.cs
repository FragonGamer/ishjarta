using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

            //public AudioSource audiosource;     //Audio source

            

            if (this.gameObject.tag == "Enemy")
            {
                Player player = FindObjectOfType<Player>();
                Enemy enemy = this.gameObject.GetComponent<Enemy>();

                enemy.ReceiveDamage(player.DealingDamage);
                enemy.AddEffectRange(player.GetCurrentEffects);
                var audiosource = enemy.GetComponent<AudioSource>();
                audiosource.Play();

                //hitsound.Play();

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
