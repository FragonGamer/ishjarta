using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int DealingDammage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetType() == typeof(BoxCollider2D) && collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Entity>().ReceiveDamage(DealingDammage);
            Destroy(this.gameObject);
        }
        // 3 is obstacle layer and 6 is wall layer
        else if (collider.gameObject.layer == 3  || collider.gameObject.layer == 6)
        {
            Destroy(this.gameObject);
        }
    }
}
