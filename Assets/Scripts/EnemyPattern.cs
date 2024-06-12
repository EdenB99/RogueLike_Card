using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern : ScriptableObject {

    public string patternName;
    public Sprite patternImage;
    public string patternText;

    public virtual void Execute(Enemy enemy)
    {

    }
    public virtual void Animate(Animator animator)
    {

    }
}

[CreateAssetMenu(fileName = "DamagePattern", menuName = "EnemyPatterns/DamagePattern")]
public class DamagePaatern : EnemyPattern
{
    
    public int DamageAmount;

    public override void Execute(Enemy enemy)
    {
        Player player = GameManager.Instance.Player;
        player.Health -= DamageAmount+enemy.AdditionalDamage;
        Debug.Log($"Player takes{DamageAmount + enemy.AdditionalDamage} damage.");
    }

    public override void Animate(Animator animator)
    {
        animator.SetTrigger("Attack");
    }
}


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
