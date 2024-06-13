using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    //------------------------------------------------------------------------------------------
    //�� ���� ���    
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
            if (value <= 0) //����Ǵ� ���� 1���� �۴ٸ�
            {
                int LeftDamge = value - armor; //���� �ƸӰ��� �ѱ丸ŭ�� ��������Ʈ�� ȣ��
                OnArmorBreak?.Invoke(LeftDamge);
                armor = 0;  //���� �ƸӰ��� 0���� ����
            }
            else //����Ǵ� ���� �׺��� ũ�� �״�� ����
            {
                armor = value;
            }
        }
    }
    Action<int> OnArmorBreak;

    public int AdditionalDamage;

    //-------------------------------------------------------------------------------
    //�� �ִϸ��̼� ���


    private Animator animator;
    CharacterController controller;

    /// <summary>
    /// �ִϸ��̼� ����
    /// </summary>
    enum AnimationState
    {
        None = 0,
        //�߰� �ʿ�
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
    //------------------------------------------------------------------------------------------
    //���� ���

    /// <summary>
    /// �����Ϳ��� �߰��� �� ������ ����Ʈ 
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
            Debug.Log("���� �̽���");
            isPattern = false;
        }
        return isPattern;
    }
    

    //------------------------------------------------------------------------------------------
    //���� �ֱ� ���

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    private void Start()
    {
        OnArmorBreak += (leftDamage) => Health -= leftDamage;
    }
    private void Die()
    {
        // �� ��� ó��
    }

    public void PerformAction(Player player)
    {
        // ���� �ൿ ����
    }
}
