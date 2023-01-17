using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    [SerializeField]
    private Transform PlayerTransform;
    private void Start()
    {
        PlayerTransform = PlayerManager.instance.player.transform;
    }
    private void LateUpdate()
    {
        if(PlayerTransform== null)
            PlayerTransform = PlayerManager.instance.player.transform;
        transform.position = PlayerTransform.position + offset;
    }
}
