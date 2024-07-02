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
    //적 스탯 요소

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
    public enum EnemyAnimationState
    {
        None = 0,
        Idle,
        Hit,
        Death,
        Attack,
        Attack2,

        //추가 필요
    }

    /// <summary>
    /// 현재 애니메이션 상태
    /// </summary>
    EnemyAnimationState state = EnemyAnimationState.None;

    /// <summary>
    /// 애니메이션 상태 설정 및 확인용 프로퍼티
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
    //패턴 요소

    /// <summary>
    /// 패턴을 실행하는 Enemy 오브젝트의 자식 스크립트
    /// </summary>
    Pattern PatternObject;
    /// <summary>
    /// 에디터에서 추가한 적 패턴의 리스트 
    /// </summary>
    public List<EnemyPattern> patterns;

    /// <summary>
    /// 전턴에 사용된 패턴
    /// </summary>
    public EnemyPattern LastPattern;

    /// <summary>
    /// 현재 설정된 패턴
    /// </summary>
    EnemyPattern CurrentPattern;

    /// <summary>
    /// 패턴을 설정하는 함수
    /// </summary>
    public void SetPattern()
    {
        if (CurrentPattern != null) //이전 패턴을 저장
        {
            LastPattern = CurrentPattern;
        }
        int Randomindex = Random.Range(0, patterns.Count);
        CurrentPattern = patterns[Randomindex];
        PatternObject.SetPattern(CurrentPattern, AdditionalDamage);
    }

    /// <summary>
    /// 패턴을 실행하는 함수
    /// </summary>
    /// <param name="patternIndex"></param>
    /// <returns></returns>
    public void ExcutePattern()
    {
        StartCoroutine(ExcutePatternCoroutine());
    }

    /// <summary>
    /// 패턴 실행시 비동기로 진행되는 적 애니메이션과 패턴 실행 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator ExcutePatternCoroutine()
    {
        
        Vector3 originalPosition = transform.position;
        Player player = GameManager.Instance.Player;
        Vector3 AttackPosition = player.transform.position + new Vector3(0.6f, 0.0f, 0.0f);
        float moveDuration = 0.5f;
        yield return StartCoroutine(MoveToPosition(AttackPosition, moveDuration)); //플레이어 앞으로 이동

        yield return StartCoroutine(PatternObject.FadeOutEffect());
        State = CurrentPattern.AnimationState; //패턴 애니메이션 실행
        yield return new WaitForSeconds(0.1f);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        CurrentPattern.Execute(this);
        yield return new WaitForSeconds(stateInfo.length);
        State = EnemyAnimationState.Idle; //애니메이션 시간 후 Idle로 변경

        
        yield return StartCoroutine(MoveToPosition(originalPosition, moveDuration));
    }

    /// <summary>
    /// 특정 위치로 현재 위치에서 경과시간만큼 천천히 이동하는 코루틴
    /// </summary>
    /// <param name="targetPosition">이동할 포지션</param>
    /// <param name="duration">경과 시간</param>
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
    //생명 주기 요소



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
        // 적의 행동 로직
    }
}
