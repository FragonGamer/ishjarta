using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        //animator.SetBool("isDead", true);
        Destroy(this.gameObject);
    }


}
