using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class DestroyOnExit : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject go = animator.transform.parent.gameObject;
        go.tag = "Untagged";
        AIPath pathfinding = go.GetComponentInParent<AIPath>();
        pathfinding.enabled = false;

        Destroy(go, stateInfo.length + 0.5f);
    }
}
