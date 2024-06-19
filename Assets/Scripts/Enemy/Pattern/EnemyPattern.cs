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
    /// <summary>
    /// 패턴에 할당된 이미지를 출력하는 함수
    /// </summary>
    /// <param name="animator"></param>
    public virtual void Animate(Animator animator)
    {

    }
}
