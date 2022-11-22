using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the state-storage for Enemy
/// </summary>
[System.Serializable]
public class EnemyData : EntityData
{
    public int enemyType;
    public EnemyData(Enemy enemy)
        : base(enemy)
    {
        this.enemyType = (int)enemy.EnemyType;
    }
}
