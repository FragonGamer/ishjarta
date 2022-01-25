using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform PlayerTransform;
    private void Awake()
    {
        PlayerTransform = GameObject.FindWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        this.transform.parent = PlayerTransform;
    }
}
