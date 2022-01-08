using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    [SerializeField] Animator animator;
    [SerializeField] int dropRate, spottingRange;
    [SerializeField] bool hasSpottedPlayer, isInRange;
    [SerializeField] Transform target;
    [SerializeField] NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GetComponent<Transform>();
    }
    public override void Attack(Vector2 vector)
    {
        throw new System.NotImplementedException();
        
    }

    protected override void Die()
    {
        animator.SetBool("isDead", true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spottingRange);
    }
}
