using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject go = animator.gameObject;
        go.tag = "Untagged";
        AIPath pathfinding = go.GetComponent<AIPath>();
        pathfinding.enabled = false;
        Destroy(animator.gameObject, stateInfo.length+0.5f);
    }
}
