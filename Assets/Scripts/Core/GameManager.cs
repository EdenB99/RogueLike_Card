using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    Player player;
    public Player Player => player;


    List<Enemy> enemies;



    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
    }

    //------------------------------------------------------------------------------------------
    //전투 요소

    private int turn;
    public int Turn
    {
        get => turn;
        set
        {
            turn = value;
            OnTurnChange?.Invoke(turn);
        }
    }
    public Action<int> OnTurnChange;
    public Action OnTurnStart;
    public Action OnTurnEnd;
   
    public void StartBattle()
    {
        turn = 0;
    }
    public void EndBattle()
    {

    }
    public void TurnStart()
    {
        OnTurnStart?.Invoke();
    }
    public void TurnEnd() 
    {
        OnTurnEnd?.Invoke();
    }
    //------------------------------------------------------------------------------------------
    //테스트 요소
    TestInput TestInput;
    //------------------------------------------------------------------------------------------
    //생명 주기
    
    private void OnEnable()
    {
        TestInput.Test.Enable();
        TestInput.Test.Test1.performed += OnTest1;
        TestInput.Test.Test2.performed += ONTest2;
    }
    private void OnDisable()
    {
        TestInput.Test.Test2.performed -= ONTest2;
        TestInput.Test.Test1.performed -= OnTest1;
        TestInput.Test.Disable();
    }
    private void Start()
    {
        TestInput = new TestInput();
        
    }


    private void OnTest1(InputAction.CallbackContext context)
    {
        TurnStart();
    }
    private void ONTest2(InputAction.CallbackContext context)
    {
        TurnEnd();
    }

}
