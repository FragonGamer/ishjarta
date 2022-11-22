using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Represents the end point of a level. For triggering the reset of a level
/// </summary>
public class EndPosition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var sc = FindObjectOfType<StageController>();
        if (collision.CompareTag("Player"))
            sc.ResetStage();
    }
}
