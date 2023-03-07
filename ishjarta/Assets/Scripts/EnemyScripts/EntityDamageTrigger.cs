using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EntityDamageTrigger : MonoBehaviour
{
[SerializeField]
float KnockbackStrength = 10f;
    public void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider is PolygonCollider2D && collider.IsTouching(GetComponent<BoxCollider2D>()))
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

                Vector2 recoilDirection = (enemy.transform.position - player.transform.position).normalized;
                enemy.GetComponent<Rigidbody2D>().AddForce(recoilDirection * KnockbackStrength, ForceMode2D.Force);
                StartCoroutine(ChangeColor(this.gameObject));
                var audiosource = enemy.GetComponent<AudioSource>();
                audiosource.Play();
                enemy.ReceiveDamage(player.DealingDamage);
                enemy.AddEffectRange(player.GetCurrentEffects);

            }
            else if (this.gameObject.tag == "Player")
            {
                Player player = this.gameObject.GetComponent<Player>();
                Enemy enemy = collider.gameObject.GetComponent<Enemy>();

                player.ReceiveDamage(enemy.DealingDamage);
                player.AddEffect(enemy.EmitEffect);

                Vector2 recoilDirection = (player.transform.position - enemy.transform.position).normalized;
                player.GetComponent<Rigidbody2D>().AddForce(recoilDirection * KnockbackStrength, ForceMode2D.Force);

                StartCoroutine(ChangeColor(this.gameObject));
            }
        }
    }

    private IEnumerator ChangeColor(GameObject go)
    {
        go.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f, 0.7f);
        yield return new WaitForSeconds(0.1f);
        go.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
