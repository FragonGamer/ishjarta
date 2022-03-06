using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    public Room room;
    private void Awake()
    {
        room = gameObject.GetComponentInParent<Room>();
    }

}
