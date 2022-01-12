using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int DealingDammage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.GetComponent<Enemy>().ReceiveDamage(DealingDammage);
            Destroy(this.gameObject);
        }
        else if (collider.gameObject.tag == "Walls" || collider.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
