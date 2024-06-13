using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamagePattern", menuName = "EnemyPatterns/DamagePattern")]
public class DamagePattern : EnemyPattern
{

    public int DamageAmount;

    public override void Execute(Enemy enemy)
    {
        Player player = GameManager.Instance.Player;
        player.Health -= DamageAmount + enemy.AdditionalDamage;
        Debug.Log($"Player takes{DamageAmount + enemy.AdditionalDamage} damage.");
    }

    public override void Animate(Animator animator)
    {
        animator.SetTrigger("Attack");
    }
}