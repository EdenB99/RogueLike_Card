using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //------------------------------------------------------------------------------------------
    //�÷��̾� ���� ���    
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
    //�÷��̾� �ִϸ��̼� ���


    private Animator animator;
    CharacterController controller;

    /// <summary>
    /// �ִϸ��̼� ����
    /// </summary>
    enum AnimationState
    {
        Idle,       // ���
        Attack,     // 2ȸ ����
        Hurt,       // �ǰ�
        Slide,      //�����̵�
        Death,       //���
        DashAttack,   //��ð���
        None        // �ʱⰪ��
    }

    /// <summary>
    /// ���� �ִϸ��̼� ����
    /// </summary>
    AnimationState state = AnimationState.None;

    /// <summary>
    /// �ִϸ��̼� ���� ���� �� Ȯ�ο� ������Ƽ
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
