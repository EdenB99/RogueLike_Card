using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DebuffPattern", menuName = "EnemyPatterns/DebuffPattern")]
public class DebuffPattern : EnemyPattern
{
    public string Debuff; // 디버프 종류

    public override void Execute(Enemy enemy)
    {
        // 디버프 논리를 여기에 추가합니다.
        Debug.Log($"Player receives {Debuff} debuff.");
    }

    public override void Animate(Animator animator)
    {
        animator.SetTrigger("Debuff");
    }
}