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
    /// 해당 패턴이 사용 가능한 지 여부
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    public virtual bool ShouldExecute(Enemy enemy)
    {
        //기본적으로 항상 실행하도록 설정
        return true;
    }
    /// <summary>
    /// 해당 패턴을 실행하는 기능 함수
    /// </summary>
    /// <param name="enemy"></param>
    public virtual void Execute(Enemy enemy)
    {

    }
}
