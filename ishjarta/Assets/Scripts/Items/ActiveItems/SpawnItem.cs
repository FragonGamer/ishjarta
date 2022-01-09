using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnItem : ActiveItem
{
    public GameObject Prefab;

    public override void Activate(GameObject parent)
    {
        var playerPos = parent.transform.position;

        Instantiate(Prefab, playerPos, Quaternion.identity);
    }
}
