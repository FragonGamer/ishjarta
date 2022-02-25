using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    private Transform PlayerTransform;
    private void Start()
    {
        PlayerTransform = PlayerManager.instance.player.transform;
    }
    private void LateUpdate()
    {
       transform.position = PlayerTransform.position + offset;
    }
}
