using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeckManager : MonoBehaviour
{
    //�� ���� ���

    public List<CardData> Allcards = new List<CardData>();
    [SerializeField]
    private List<CardData> DrawPile;
    [SerializeField]
    private List<CardData> DiscardPile;
    [SerializeField]
    private List<CardData> hand;
    public List<CardData> Hand
    {
        get => hand;
        set
        {
            hand = value;
            HandChange(hand);
        }
    }
    public Action<List<CardData>> HandChange;

    /// <summary>
    /// �ִ� �ڵ� ����
    /// </summary>
    public int MaxHandSize = 5;

    Player player;

    private void Start()
    {
        DrawPile = new List<CardData>();
        DiscardPile = new List<CardData>();
        hand = new List<CardData>();
        player = GameManager.Instance.Player;
    }



    //----------------------------------------------------------------------------------------------------
    //�� ��ȣ�ۿ� �Լ�
    public void InitializeDeck()
    {
        DrawPile.Clear();
        DrawPile.AddRange(Allcards);
        Shuffle(DrawPile);
        DiscardPile.Clear();
        Hand.Clear();
        for (int i = 0; i<MaxHandSize; i++)
        {
            DrawCard();
        }
    }


    /// <summary>
    /// ȣ���� ���� ī�带 ���� �Լ�
    /// </summary>
    /// <param name="deck">������ ��</param>
    public void Shuffle(List<CardData> deck)
    {
        for (int i = 0; i < deck.Count; i++) //�Ǽ� ������ ����
        {
            CardData tempCard = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = tempCard;
        }
    }

    /// <summary>
    /// ���� ���̿� ������ ���̸� �߰��� ���� ���� �Լ�
    /// </summary>
    public bool Reshuffle()
    {
        if (DiscardPile.Count != 0)
        {
            DrawPile.AddRange(DiscardPile);
            DiscardPile.Clear();
            Shuffle(DrawPile);
            return true;
        }
        else Debug.Log("���� ī����̿� ī�尡 �����ϴ�."); return false;

    }

    /// <summary>
    /// ī�带 �̰� ���� ī�带 ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>���� ī��</returns>
    public CardData DrawCard()
    {
        if (hand.Count >= MaxHandSize)
        {
            Debug.Log("hand is full.");
            CardData DisCard = DrawPile[0];
            DrawPile.RemoveAt(0); //���� ���̿��� ����
            DiscardPile.Add(DisCard); //���� ī�忡 �߰�
            return null;

        }
        if (DrawPile.Count == 0) //���� ���̿� ī�尡 ������ ���� ����
        {
            if (!Reshuffle()) //���� ī��� ���� ī�带 ���� ���� ���� ī�忡�� ī�尡 ������
            {
                Debug.Log("�� ī�� ���̿� ī�尡 �����ϴ�.");
            }
        }
        if (DrawPile.Count == 0) //���� �Ŀ��� ���� ī�尡 ������ null ��ȯ
        {
            return null;
        }
        CardData drawnCard = DrawPile[0];
        DrawPile.RemoveAt(0); //���� ���̿��� ����
        hand.Add(drawnCard); //�ڵ� ���̿� �߰�
        return drawnCard; //���� ī�� ��ȯ
    }

    /// <summary>
    /// ī�带 ������ �Լ�
    /// </summary>
    /// <param name="card">���� ī��</param>
    public CardData DiscardCard(CardData card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
            DiscardPile.Add(card);
            return card;
        }
        else return null;
    }

    /// <summary>
    /// ȣ��� ī�带 �ڵ忡�� ���
    /// </summary>
    /// <param name="card">����� ī��</param>
    /// <param name="enemy">���õ� �� ��ü</param>
    public void PlayCard(CardData card, Enemy enemy)
    {
        if (hand.Contains(card)) //�ڵ� ���̿� �ش� ī�尡 �����ϴ��� Ȯ��
        {
            if (player.Energy >= card.Cost)
            {
                player.Energy -= card.Cost;
                hand.Remove(card);

                //ī�� ������ ���� ���ǹ� �߰��ʿ� *�Ҹ� ���� Ű����
                DiscardPile.Add(card);
                //ī���� ����� �����ؾ� �� !!!!!
            }
        }
    }
    
    /// <summary>
    /// ���� �� ��Ȳ�� ����׷� ����
    /// </summary>
    public void PrintDeckStatus()
    {
        Debug.Log("Draw Pile: " + DrawPile.Count);
        Debug.Log("Discard Pile: " + DiscardPile.Count);
        Debug.Log("hand: " + hand.Count);
    }
}
