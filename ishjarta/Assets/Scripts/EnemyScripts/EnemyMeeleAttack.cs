using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeeleAttack : MonoBehaviour
{
    [SerializeField] public int MeeleDamage;
    [SerializeField] public float AttackRate;
    private float time = 0.0f;

    private CircleCollider2D circleCollider;
    private Enemy enemyScript;
    private Collider2D playerCollider;

    private void Awake()
    {
        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
        enemyScript = gameObject.GetComponent<Enemy>();
    }
    private void Start()
    {
        AttackRate = enemyScript.GetAttackRate();
        MeeleDamage = (int)Math.Round(enemyScript.GetDamage());
        Debug.Log(circleCollider.radius);
        circleCollider.radius = enemyScript.GetRange();
        Debug.Log(circleCollider.radius);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyScript.isInRange = true;
            playerCollider = collision;
        }
    }
    private void Update()
    {
        if (enemyScript.isInRange == true)
        {
                if (playerCollider != null)
                {
                    if (time >= AttackRate)
                    {
                        time = 0.0f;

                        // Add effect to the player
                        playerCollider.gameObject.GetComponent<Player>().AddEffect(enemyScript.EmitEffect);

                        playerCollider.gameObject.GetComponent<Player>().ReceiveDamage(MeeleDamage);
                    }
                    time += Time.deltaTime;
                }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyScript.isInRange = false;
            playerCollider = null;
        }
    }
}
