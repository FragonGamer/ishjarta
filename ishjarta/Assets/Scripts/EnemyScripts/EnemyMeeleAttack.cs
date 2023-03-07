using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeeleAttack : MonoBehaviour
{
    [SerializeField] public Animator animator;
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

                    //Add effect to the player
                    //playerCollider.gameObject.GetComponent<Player>().AddEffect(enemyScript.EmitEffect);

                    //playerCollider.gameObject.GetComponent<Player>().ReceiveDamage(MeeleDamage);

                    Vector2 lookdir = playerCollider.gameObject.transform.position
                    - enemyScript.gameObject.transform.position;
                    float angle = Mathf.Atan2(lookdir.y, lookdir.x);
                    if (angle < 0)
                        angle = (float)(Math.PI - Math.Abs(angle) + Math.PI);

                    Vector2[] v = new Vector2[]
                    {
                                new Vector2 {x = 0, y = 0},
                                Entity.RotateVector2(new Vector2 {x = enemyScript.Range * (0.7f), y = enemyScript.Width}, angle),
                                Entity.RotateVector2(new Vector2 {x = enemyScript.Range, y = 0}, angle),
                                Entity.RotateVector2(new Vector2 {x = enemyScript.Range * (0.7f), y = enemyScript.Width * (-1)}, angle)
                    };

                    //Debug.Log($"Angle: {angle}, x: {v[2].x}, y: {v[2].y}");
                    if (animator != null)
                    {
                        try
                        {
                            animator.SetBool("attack", true);
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    PolygonCollider2D pc = this.gameObject.AddComponent<PolygonCollider2D>();
                    pc.isTrigger = true;
                    pc.points = v;

                    Destroy(pc, 0.2f);
                }
                if (animator != null)
                {
                    try
                    {
                        animator.SetBool("attack", false);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
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
