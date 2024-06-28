using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    //카드 사용효과를 패널-> 덱->카드순으로 구현
    //카드 오브젝트 풀 구현
    //
    Player player;
    public Player Player => player;

    DeckManager deck;

    public DeckManager Deck => deck;
    
    public List<Enemy> enemies;
    


    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
        deck = GetComponent<DeckManager>();    
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
        player.Health = player.Maxhealth;
        Deck.InitializeDeck();
        enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
        TurnStart();
    }
    public void EndBattle()
    {

    }
    public void TurnStart()
    {
        Turn++;
        for (int i = 0; Deck.MaxHandSize > Deck.Hand.Count; i++) //최대 핸드 수만큼 카드를 버림
        {
            Deck.DrawCard();
        }
        player.Energy = player.maxEnergy;
        OnTurnStart?.Invoke();
    }
    public void TurnEnd() 
    {
        foreach(CardData cardData in Deck.Hand) //현재 핸드에 있는 카드수만큼 버림
        {
            Deck.DiscardCard(cardData);
        }
        OnTurnEnd?.Invoke();
    }
    //------------------------------------------------------------------------------------------
    //카드 오브젝트 생성 요소







    //------------------------------------------------------------------------------------------
    //테스트 요소
    TestInput TestInput;




    //------------------------------------------------------------------------------------------
    //생명 주기
    
    
    private void OnEnable()
    {
        TestInput = new TestInput();
        TestInput.Test.Enable();
        TestInput.Test.Test1.performed += OnTest1;
        TestInput.Test.Test2.performed += ONTest2;
        TestInput.Test.Test3.performed += OnTest3;
        TestInput.Test.Test4.performed += OnTest4;
    }

 

    private void OnDisable()
    {
        TestInput.Test.Test4.performed -= OnTest4;
        TestInput.Test.Test3.performed -= OnTest3;
        TestInput.Test.Test2.performed -= ONTest2;
        TestInput.Test.Test1.performed -= OnTest1;
        TestInput.Test.Disable();
    }
    private void Start()
    {
        TestInput = new TestInput();
        OnInitialize();
    }
    

    private void OnTest1(InputAction.CallbackContext context)
    {
        TurnStart();
    }
    private void ONTest2(InputAction.CallbackContext context)
    {
        TurnEnd();
    }
    private void OnTest3(InputAction.CallbackContext context)
    {
        StartBattle();
    }

    private void OnTest4(InputAction.CallbackContext context)
    {
        EndBattle();
    }


}
