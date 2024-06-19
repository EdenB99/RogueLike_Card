using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurnBasedPattern", menuName = "EnemyPatterns/TurnBasedPattern")]
public class TurnBasedPattern : EnemyPattern
{
    public int turnThreshold;

    public override bool ShouldExecute(Enemy enemy)
    {
        return GameManager.Instance.Turn >= turnThreshold;
    }

    public override void Execute(Enemy enemy)
    {
        // 패턴 실행 로직
        Debug.Log($"Executing {this} because turn is {GameManager.Instance.Turn}");
    }

    public override void Animate(Animator animator)
    {
        animator.SetTrigger("TurnBased");
    }
}
