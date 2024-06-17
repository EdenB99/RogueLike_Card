using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    //------------------------------------------------------------------------------------------
    //적 스탯 요소    
    private int health;
    public int Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
            {
                health = 0;
                Die();
            }
        }
    }
    private int armor;
    public int Armor
    {
        get => armor;
        set
        {
            if (value <= 0) //변경되는 값이 1보다 작다면
            {
                int LeftDamge = value - armor; //현재 아머값을 넘긴만큼을 델리게이트로 호출
                OnArmorBreak?.Invoke(LeftDamge);
                armor = 0;  //그후 아머값을 0으로 변경
            }
            else //변경되는 값이 그보다 크면 그대로 변경
            {
                armor = value;
            }
        }
    }
    Action<int> OnArmorBreak;

    public int AdditionalDamage;

    //-------------------------------------------------------------------------------
    //적 애니메이션 요소


    private Animator animator;

    /// <summary>
    /// 애니메이션 상태
    /// </summary>
    enum AnimationState
    {
        None = 0,
        //추가 필요
    }

    /// <summary>
    /// 현재 애니메이션 상태
    /// </summary>
    AnimationState state = AnimationState.None;

    /// <summary>
    /// 애니메이션 상태 설정 및 확인용 프로퍼티
    /// </summary>
    AnimationState State
    {
        get => state;
        set
        {
            if (value != state)
            {
                state = value;
                animator.SetTrigger(state.ToString());
            }
        }
    }
    //------------------------------------------------------------------------------------------
    //패턴 요소

    private SpriteRenderer PatternImage;
    private TextMeshProUGUI PatternText;

    /// <summary>
    /// 에디터에서 추가한 적 패턴의 리스트 
    /// </summary>
    public List<EnemyPattern> patterns;


    public bool ExcutePattern(int patternIndex)
    {
        bool isPattern = false;
        if (patternIndex >= 0 && patternIndex < patterns.Count)
        {
            if (patterns[patternIndex].ShouldExecute(this))
            {
                patterns[patternIndex].Execute(this);
                patterns[patternIndex].Animate(animator);
                isPattern = true;
            }
        }else
        {
            Debug.Log("패턴 미실행");
            isPattern = false;
        }
        return isPattern;
    }
    
    private void ShowPatterUI(int patternIndex)
    {
        EnemyPattern enemyPattern = patterns[patternIndex];
        PatternImage.sprite = enemyPattern.patternImage;
        switch (enemyPattern.patternType)
        {
            case PatternType.Damage:
                DamagePattern damagePattern = enemyPattern as DamagePattern;
                if (damagePattern != null )
                {
                    PatternText.text = damagePattern.CurrentDamage.ToString();
                }
                break;
            default:
                PatternText.text = null;
                break;
        }
        //패턴 UI을 구성하고 보여줄 곳
    }

    //------------------------------------------------------------------------------------------
    //생명 주기 요소

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Transform Child = transform.GetChild(0);
        PatternImage = Child.GetChild(0).GetComponent<SpriteRenderer>();
        PatternText = Child.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        OnArmorBreak += (leftDamage) => Health -= leftDamage;
        ShowPatterUI(0);
    }
    private void Die()
    {
        // 적 사망 처리
    }

    public void PerformAction(Player player)
    {
        // 적의 행동 로직
    }
}
