using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern : ScriptableObject {

    public string patternName;
    public Sprite patternImage;
    public string patternText;

    public virtual bool ShouldExecute(Enemy enemy)
    {
        //�⺻������ �׻� �����ϵ��� ����
        return true;
    }
    public virtual void Execute(Enemy enemy)
    {

    }
    public virtual void Animate(Animator animator)
    {

    }
}
