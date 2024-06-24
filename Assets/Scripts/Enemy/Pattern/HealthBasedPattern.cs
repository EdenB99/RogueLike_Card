using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthBasedPattern", menuName = "EnemyPatterns/HealthBasedPattern")]
public class HealthBasedPattern : EnemyPattern
{
    public int healthThreshold;

    public override bool ShouldExecute(Enemy enemy)
    {
        return enemy.Health <= healthThreshold;
    }

    public override void Execute(Enemy enemy)
    {
        // 패턴 실행 로직
        Debug.Log($"Executing {this} because health is {enemy.Health}");
    }

}