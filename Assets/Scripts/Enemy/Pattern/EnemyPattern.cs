using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string patternText;
    public PatternType patternType;
    
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
    /// <summary>
    /// ���Ͽ� �Ҵ�� �̹����� ����ϴ� �Լ�
    /// </summary>
    /// <param name="animator"></param>
    public virtual void Animate(Animator animator)
    {

    }
}
