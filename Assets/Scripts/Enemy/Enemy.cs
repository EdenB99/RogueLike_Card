using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


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

    Pattern PatternObject;
    /// <summary>
    /// �����Ϳ��� �߰��� �� ������ ����Ʈ 
    /// </summary>
    public List<EnemyPattern> patterns;

    /// <summary>
    /// ������ �����ϴ� �Լ�
    /// </summary>
    public void SetPattern()
    {
        PatternObject.SetPattern(patterns[0], AdditionalDamage);
    }

    /// <summary>
    /// ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="patternIndex"></param>
    /// <returns></returns>
    public void ExcutePattern()
    {
        
    }
   

    //------------------------------------------------------------------------------------------
    //���� �ֱ� ���



    private void Awake()
    {
        animator = GetComponent<Animator>();
        PatternObject = GetComponentInChildren<Pattern>(); 
    }
    private void Start()
    {
        OnArmorBreak += (leftDamage) => Health -= leftDamage;
        

        GameManager.Instance.OnTurnStart += SetPattern;
        GameManager.Instance.OnTurnEnd += ExcutePattern;
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
