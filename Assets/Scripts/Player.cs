using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //------------------------------------------------------------------------------------------
    //플레이어 스탯 요소    
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
                OnPlayerDie?.Invoke();
            }
            else OnHealthChange?.Invoke(health);

        }
    }
    Action<int> OnHealthChange;
    Action OnPlayerDie;

    private int Armor;

    //-----------------------------------------------------------------------------------------
    //플레이어 애니메이션 요소


    private Animator animator;
    CharacterController controller;

    /// <summary>
    /// 애니메이션 상태
    /// </summary>
    enum AnimationState
    {
        Idle,       // 대기
        Attack,     // 2회 공격
        Hurt,       // 피격
        Slide,      //슬라이드
        Death,       //사망
        DashAttack,   //대시공격
        None        // 초기값용
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
}
