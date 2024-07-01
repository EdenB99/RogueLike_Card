using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    //ī�� ���ȿ���� �г�-> ��->ī������� ����
    //ī�� ������Ʈ Ǯ ����
    //
    Player player;
    public Player Player => player;

    DeckManager deck;

    public DeckManager Deck => deck;
    
    public List<Enemy> enemies;
    


    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        if (player != null )
        {
            Debug.Log("Player get");
        }
        enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
        deck = GetComponent<DeckManager>();    
    }

    //------------------------------------------------------------------------------------------
    //���� ���

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
        
        player.Health = player.Maxhealth;

        Deck.InitializeDeck();
        turn = 0;
        enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
        TurnStart();
    }
    public void EndBattle()
    {

    }
    public void TurnStart()
    {
        Turn++;
        while (Deck.Hand.Count < Deck.MaxHandSize)
        {
            CardData drawnCard = Deck.DrawCard();
            if (drawnCard == null)
            {
                Debug.Log("No more cards to draw or hand is full.");
                break; // ��ο� ���� �� ���� Ż��
            }
        }
        player.Energy = player.maxEnergy;
        OnTurnStart?.Invoke();
    }
    public void TurnEnd() 
    {
        foreach(CardData cardData in Deck.Hand) //���� �ڵ忡 �ִ� ī�����ŭ ����
        {
            Deck.DiscardCard(cardData);
        }
        OnTurnEnd?.Invoke();
    }
    //------------------------------------------------------------------------------------------
    //ī�� ������Ʈ ���� ���







    //------------------------------------------------------------------------------------------
    //�׽�Ʈ ���
    TestInput TestInput;




    //------------------------------------------------------------------------------------------
    //���� �ֱ�
    
    
    private void OnEnable()
    {
        TestInput = new TestInput();
        TestInput.Test.Enable();
        TestInput.Test.Test1.performed += OnTest1;
        TestInput.Test.Test2.performed += OnTest2;
        TestInput.Test.Test3.performed += OnTest3;
        TestInput.Test.Test4.performed += OnTest4;
    }

 

    private void OnDisable()
    {
        TestInput.Test.Test4.performed -= OnTest4;
        TestInput.Test.Test3.performed -= OnTest3;
        TestInput.Test.Test2.performed -= OnTest2;
        TestInput.Test.Test1.performed -= OnTest1;
        TestInput.Test.Disable();
    }
    private void Awake()
    {
        TestInput = new TestInput();
        OnInitialize();
    }
    private void Start()
    {
        
    }
    

    private void OnTest1(InputAction.CallbackContext context)
    {
        TurnStart();
    }
    private void OnTest2(InputAction.CallbackContext context)
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
