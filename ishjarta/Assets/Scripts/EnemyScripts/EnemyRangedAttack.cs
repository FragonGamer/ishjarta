using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyRangedAttack : MonoBehaviour
{
    [field: SerializeField] GameObject FirePoint { get; set; }
    [field: SerializeField] string ProjectileNameInFile { get; set; }
    [field: SerializeField] float ProjectileVelocity { get; set; }
    [SerializeField] public int RangedDamage;
    [SerializeField] public float AttackRate;

    private float time = 0.0f;
    private CircleCollider2D circleCollider;
    private Enemy enemyScript;
    private Transform target;
    private Collider2D playerCollider;

    private void Awake()
    {
        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
        enemyScript = gameObject.GetComponent<Enemy>();
    }
    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        AttackRate = enemyScript.GetAttackRate();
        RangedDamage = (int)Math.Round(enemyScript.GetDamage());
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
        if (enemyScript.isInRange == true && playerCollider != null)
        {
            if (time >= AttackRate)
            {
                Debug.Log("Raycasting");
                var hit = Physics2D.Raycast(transform.position, target.position - transform.position, Mathf.Infinity, LayerMask.GetMask("Player"));
                Debug.Log(hit);
                Debug.Log(hit.collider.tag);
                if (hit && hit.collider.tag == "Player")
                {
                    Debug.Log("Hit a player");
                    float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

                    FirePoint.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                    GameObject projectile = Instantiate((GameObject)Resources.Load($"Prefabs/Projectiles/{ProjectileNameInFile}"),
                    (Quaternion.Euler(0f, 0f, angle) * (FirePoint.transform.position - transform.position)) + transform.position, FirePoint.transform.rotation);

                    // Add effect to the player
                    projectile.GetComponent<Projectile>().EmitEffects.Add(enemyScript.EmitEffect);

                    projectile.GetComponent<Projectile>().DealingDammage = RangedDamage;
                    projectile.GetComponent<Rigidbody2D>().AddForce((FirePoint.transform.up) * ProjectileVelocity, ForceMode2D.Impulse);

                    Destroy(projectile, 10f);
                }
                time = 0.0f;
            }
            time += Time.deltaTime;
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