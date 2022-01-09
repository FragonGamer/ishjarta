using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActiveItem : Item
{
    public float cooldownTime;
    public float activeTime;

    // Line 11 ist für Testzwecke, später löschen (einfach mich anheulen - Ian)
    public float dashVelocity;

    public virtual void Activate(GameObject parent) 
    {
        // Alles in dieser Klasse ist für Testzwecke, später löschen (einfach mich anheulen - Ian)
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        Rigidbody2D rigidBody = parent.GetComponent<Rigidbody2D>();

        rigidBody.velocity = movement.movement.normalized * dashVelocity;
    }
}
