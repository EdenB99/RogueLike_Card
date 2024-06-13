using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DebuffPattern", menuName = "EnemyPatterns/DebuffPattern")]
public class DebuffPattern : EnemyPattern
{
    public string Debuff; // ����� ����

    public override void Execute(Enemy enemy)
    {
        // ����� ���� ���⿡ �߰��մϴ�.
        Debug.Log($"Player receives {Debuff} debuff.");
    }

    public override void Animate(Animator animator)
    {
        animator.SetTrigger("Debuff");
    }
}