using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{
    //------------------------------------------------------------------------------------------
    //�� ���� ���

    public int Maxhealth = 30;
    [SerializeField]
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
                DeathCoroutine();
            }
            else StartCoroutine(HitCoroutine());
        }
    }


    private IEnumerator HitCoroutine()
    {
        State = EnemyAnimationState.Hit;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        State = EnemyAnimationState.Idle;
    }

    private IEnumerator DeathCoroutine()
    {
        State = EnemyAnimationState.Death;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        Destroy(gameObject);
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
    public enum EnemyAnimationState
    {
        None = 0,
        Idle,
        Hit,
        Death,
        Attack,
        Attack2,

        //�߰� �ʿ�
    }

    /// <summary>
    /// ���� �ִϸ��̼� ����
    /// </summary>
    EnemyAnimationState state = EnemyAnimationState.None;

    /// <summary>
    /// �ִϸ��̼� ���� ���� �� Ȯ�ο� ������Ƽ
    /// </summary>
    EnemyAnimationState State
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
    /// ������ �����ϴ� Enemy ������Ʈ�� �ڽ� ��ũ��Ʈ
    /// </summary>
    Pattern PatternObject;
    /// <summary>
    /// �����Ϳ��� �߰��� �� ������ ����Ʈ 
    /// </summary>
    public List<EnemyPattern> patterns;

    /// <summary>
    /// ���Ͽ� ���� ����
    /// </summary>
    public EnemyPattern LastPattern;

    /// <summary>
    /// ���� ������ ����
    /// </summary>
    EnemyPattern CurrentPattern;

    /// <summary>
    /// ������ �����ϴ� �Լ�
    /// </summary>
    public void SetPattern()
    {
        if (CurrentPattern != null) //���� ������ ����
        {
            LastPattern = CurrentPattern;
        }
        int Randomindex = Random.Range(0, patterns.Count);
        CurrentPattern = patterns[Randomindex];
        PatternObject.SetPattern(CurrentPattern, AdditionalDamage);
    }

    /// <summary>
    /// ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="patternIndex"></param>
    /// <returns></returns>
    public void ExcutePattern()
    {
        StartCoroutine(ExcutePatternCoroutine());
    }

    /// <summary>
    /// ���� ����� �񵿱�� ����Ǵ� �� �ִϸ��̼ǰ� ���� ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator ExcutePatternCoroutine()
    {
        
        Vector3 originalPosition = transform.position;
        Player player = GameManager.Instance.Player;
        Vector3 AttackPosition = player.transform.position + new Vector3(0.6f, 0.0f, 0.0f);
        float moveDuration = 0.5f;
        yield return StartCoroutine(MoveToPosition(AttackPosition, moveDuration)); //�÷��̾� ������ �̵�

        yield return StartCoroutine(PatternObject.FadeOutEffect());
        State = CurrentPattern.AnimationState; //���� �ִϸ��̼� ����
        yield return new WaitForSeconds(0.1f);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        CurrentPattern.Execute(this);
        yield return new WaitForSeconds(stateInfo.length);
        State = EnemyAnimationState.Idle; //�ִϸ��̼� �ð� �� Idle�� ����

        
        yield return StartCoroutine(MoveToPosition(originalPosition, moveDuration));
    }

    /// <summary>
    /// Ư�� ��ġ�� ���� ��ġ���� ����ð���ŭ õõ�� �̵��ϴ� �ڷ�ƾ
    /// </summary>
    /// <param name="targetPosition">�̵��� ������</param>
    /// <param name="duration">��� �ð�</param>
    /// <returns></returns>
    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    //------------------------------------------------------------------------------------------
    //���� �ֱ� ���



    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            Debug.Log("animator is ready");
        }
        PatternObject = GetComponentInChildren<Pattern>(); 
    }
    private void Start()
    {
        OnArmorBreak += (leftDamage) => Health -= leftDamage;
        GameManager.Instance.OnTurnStart += SetPattern;
        GameManager.Instance.OnTurnEnd += ExcutePattern;

        health = Maxhealth;
    }
    
    public void PerformAction(Player player)
    {
        // ���� �ൿ ����
    }
}
