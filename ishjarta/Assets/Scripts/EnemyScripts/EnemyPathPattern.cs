using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathPattern : MonoBehaviour
{
    [SerializeField] AIPath aiPath;

    void Start()
    {
        aiPath.alwaysDrawGizmos = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
