using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.AI;

public class Enemy : Entity
{
    [SerializeField] Animator animator;

    [SerializeField] private int spottingRange = 1;
    public int dropRate { get; private set; }
    public bool hasSpottedPlayer { get; set; }
    public bool isInRange { get; set; }
    private float time;
    #region getters and setters
    public int GetMovementSpeed()
    {
        return movementSpeed;
    }
    public int GetRange()
    {
        return range;
    }
    public int GetSpottingRange()
    {
        return spottingRange;
    }
    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override void Attack(Vector2 vector)
    {
        throw new System.NotImplementedException();
        
    }

    protected override void Die()
    {
        if (gameObject.GetComponent<CircleCollider2D>() != null)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        if (gameObject.GetComponent<BoxCollider2D>() != null)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        gameObject.GetComponent<AIPath>().canMove = false;
        animator.SetBool("isDead", true);

    }


}
