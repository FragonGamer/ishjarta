using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int DealingDammage;
    public GameObject Owner;

    // Effects which will be passed on to the enemy
    public List<BaseEffect> EmitEffects { get; set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == null)
            return;
        if (collider.GetType() == typeof(BoxCollider2D) && (!Owner.CompareTag("Enemy") && collider.gameObject.tag == "Enemy" || !Owner.CompareTag("Player") && collider.gameObject.tag == "Player"))
        {
            Entity entity = collider.gameObject.GetComponent<Entity>();
            if (entity != null)
            {
                entity.ReceiveDamage(DealingDammage);
                entity.AddEffectRange(EmitEffects);
                Destroy(this.gameObject);
            }
        }
        // 3 is obstacle layer and 6 is wall layer
        else if (collider.gameObject.layer == 3 || collider.gameObject.layer == 6 || collider.CompareTag("Obstacle") || collider.CompareTag("Door"))
        {
            Destroy(this.gameObject);
        }
    }
}
