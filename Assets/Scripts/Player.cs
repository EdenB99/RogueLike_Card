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
            } else //변경되는 값이 그보다 크면 그대로 변경
            {
                armor = value;
            }
        }
    }
    Action<int> OnArmorBreak;


    private int energy;
    public int Energy
    {
        get => energy;
        set
        {
            energy = value;
            OnEnergyChange?.Invoke(energy);
        }
    }
    Action<int> OnEnergyChange;

    //-------------------------------------------------------------------------------
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



    private void Awake()
    {
        OnArmorBreak += (leftDamage) => Health -= leftDamage;
    }
}
