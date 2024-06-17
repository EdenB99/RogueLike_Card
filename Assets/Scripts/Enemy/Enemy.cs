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

    private SpriteRenderer PatternImage;
    private TextMeshProUGUI PatternText;

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
        //���� UI�� �����ϰ� ������ ��
    }

    //------------------------------------------------------------------------------------------
    //���� �ֱ� ���

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
        // �� ��� ó��
    }

    public void PerformAction(Player player)
    {
        // ���� �ൿ ����
    }
}
