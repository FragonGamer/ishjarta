using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int DealingDammage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy" && collider.GetType() == typeof(BoxCollider2D))
        {
            collider.gameObject.GetComponent<Enemy>().ReceiveDamage(DealingDammage);
            Destroy(this.gameObject);
        }
        // 3 is obstacle layer and 6 is wall layer
        else if (collider.gameObject.layer == 3  || collider.gameObject.layer == 6)
        {
            Destroy(this.gameObject);
        }
    }
}
