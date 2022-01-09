using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashItem : ActiveItem
{
    public float dashSpeed;

    public override void Activate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        Rigidbody2D rigidBody = parent.GetComponent<Rigidbody2D>();

        rigidBody.velocity = movement.movement.normalized * dashSpeed;
    }
}
