using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamagePattern", menuName = "EnemyPatterns/DamagePattern")]
public class DamagePattern : EnemyPattern
{
    public int DamageAmount;
    [HideInInspector]
    public int CurrentDamage;

    public override void Execute(Enemy enemy)
    {
        CurrentDamage = DamageAmount + enemy.AdditionalDamage;
        Player player = GameManager.Instance.Player;
        player.Health -= CurrentDamage;
        Debug.Log($"Player takes{CurrentDamage} damage.");
    }

    public override void Animate(Animator animator)
    {
        animator.SetTrigger("Attack");
    }
}