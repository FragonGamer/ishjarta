using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu]
public class DashItem : ActiveItem
{
    public float dashSpeed;



    public override void Activate(GameObject parent)
    {
        PlayerController movement = parent.GetComponent<PlayerController>();
        Rigidbody2D rigidBody = parent.GetComponent<Rigidbody2D>();

        rigidBody.velocity = movement.GetMovementVector().normalized * dashSpeed;
    }
}
