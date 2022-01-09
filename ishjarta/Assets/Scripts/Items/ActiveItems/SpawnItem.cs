using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnItem : ActiveItem
{
    public GameObject Prefab;

    public override void Activate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();

        Instantiate(Prefab, movement.movement, Quaternion.identity);
    }
}
