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
        aiPath.maxSpeed = enemyScript.GetMovementSpeed();
        aiPath.endReachedDistance = enemyScript.GetRange()-0.2f;
        aiPath.whenCloseToDestination = CloseToDestinationMode.Stop;
    }
    private void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);
        if (distance <= enemyScript.GetSpottingRange() + 3f)
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
