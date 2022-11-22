using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Represents the starting position of a level where the player will be spawned. Should only exist once.
/// </summary>
public class StartPosition : MonoBehaviour
{
    public Room room;
    private void Awake()
    {
        room = gameObject.GetComponentInParent<Room>();
    }

}
