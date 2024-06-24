using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public enum PatternType
{
    Damage,
    Debuff,
    HealthBase,
    TurnBase,

}
public class EnemyPattern : ScriptableObject {
    public int DamageAmount;
    public Sprite patternImage;
    public PatternType patternType;
    public EnemyAnimationState AnimationState;

    /// <summary>
    /// �ش� ������ ��� ������ �� ����
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    public virtual bool ShouldExecute(Enemy enemy)
    {
        //�⺻������ �׻� �����ϵ��� ����
        return true;
    }
    /// <summary>
    /// �ش� ������ �����ϴ� ��� �Լ�
    /// </summary>
    /// <param name="enemy"></param>
    public virtual void Execute(Enemy enemy)
    {

    }
}
