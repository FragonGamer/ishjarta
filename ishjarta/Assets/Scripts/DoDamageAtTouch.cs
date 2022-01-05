using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageAtTouch : MonoBehaviour
{
    [SerializeField] public int TouchDamage = 20;
    [SerializeField] public float DamageInterval = 1;
    private float time = 0.0f;

    private GameObject player;

    private Enemy enemyScript;

    private void Start()
    {
        enemyScript = gameObject.GetComponent<Enemy>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        time = DamageInterval;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (time >= DamageInterval)
            {
                time = 0.0f;
                collision.gameObject.GetComponent<Player>().ReceiveDamage(TouchDamage);
            }
            time += Time.deltaTime;
        }
    }
}
