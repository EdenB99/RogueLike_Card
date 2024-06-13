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
        // ���� ���� ����
        Debug.Log($"Executing {patternName} because health is {enemy.Health}");
    }

    public override void Animate(Animator animator)
    {
        animator.SetTrigger("HealthBased");
    }
}