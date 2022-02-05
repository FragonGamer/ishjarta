using Pathfinding;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    [SerializeField] AIPath aiPath;
    [SerializeField] AIDestinationSetter aiDestinationSetter;
    [SerializeField] Transform target;
    [SerializeField] Enemy enemyScript;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    float distance;
    public float StoppingDistance { get; set; } = 0.5f;
    #region Getters and Setters
    #endregion

    private void Awake()
    {
        aiPath = GetComponent<AIPath>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        enemyScript = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        aiPath.canMove = false;
        target = PlayerManager.instance.player.transform;
        aiDestinationSetter.target = target;
        aiPath.maxSpeed = enemyScript.GetSpeed();
        aiPath.endReachedDistance = ((enemyScript.GetRange()/2) - StoppingDistance) <= 0 ? 0.5f : (enemyScript.GetRange() / 2) - StoppingDistance;
        aiPath.whenCloseToDestination = CloseToDestinationMode.Stop;
    }
    private void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);
        if (distance <= enemyScript.GetSpottingRange() + enemyScript.GetSpottingRange()/10)
        {
            animator.SetBool("hasSpottedPlayer",true);
            animator.SetFloat("Speed",aiPath.velocity.sqrMagnitude);
            if (distance <= enemyScript.GetSpottingRange())
            {
                aiPath.canMove = true;
                if (distance <= enemyScript.GetRange())
                {
                    enemyScript.isInRange = true;
                }
                else if (enemyScript.isInRange)
                {
                    enemyScript.isInRange = false;
                }
            }
        }
    }
}
