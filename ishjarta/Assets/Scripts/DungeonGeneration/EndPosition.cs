using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPosition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var sc = FindObjectOfType<StageController>();
        if (collision.CompareTag("Player"))
            sc.ResetStage();
    }
}
