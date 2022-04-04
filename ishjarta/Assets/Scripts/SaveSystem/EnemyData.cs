using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
