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
    private CircleCollider2D circleCollider;
    private Enemy enemyScript;

    private void Awake()
    {
        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
        enemyScript = gameObject.GetComponent<Enemy>();
    }
    private void Start()
    {
        Debug.Log(circleCollider.radius);
        circleCollider.radius = enemyScript.GetRange();
        Debug.Log(circleCollider.radius);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyScript.isInRange = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (time >= DamageInterval)
            {
                time = 0.0f;
                other.gameObject.GetComponent<Player>().ReceiveDamage(TouchDamage);
            }
            time += Time.deltaTime;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyScript.isInRange = false;
    }
}
