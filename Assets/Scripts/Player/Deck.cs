using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Deck : MonoBehaviour
{
    //�� ���� ���

    public List<CardData> Allcards = new List<CardData>();
    private List<CardData> DrawPile;
    private List<CardData> DiscardPile;
    private List<CardData> Hand;

    /// <summary>
    /// �ִ� �ڵ� ����
    /// </summary>
    private const int MaxHandSize = 10;

    Player player;

    private void Start()
    {
        DrawPile = new List<CardData>(Allcards);
        DiscardPile = new List<CardData>();
        Hand = new List<CardData>();
        Shuffle(DrawPile);
        player = GameManager.Instance.Player;
    }



    //----------------------------------------------------------------------------------------------------
    //�� ��ȣ�ۿ� �Լ�


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
    public void Reshuffle()
    {
        DrawPile.AddRange(DiscardPile);
        DiscardPile.Clear();
        Shuffle(DrawPile);
    }

    /// <summary>
    /// ī�带 �̰� ���� ī�带 ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>���� ī��</returns>
    public CardData DrawCard()
    {
        if (Hand.Count >= MaxHandSize)
        {
            Debug.Log("Hand is full.");
            CardData DisCard = DrawPile[0];
            DrawPile.RemoveAt(0); //���� ���̿��� ����
            DiscardPile.Add(DisCard); //���� ī�忡 �߰�
            return null;

        }
        if (DrawPile.Count == 0) //���� ���̿� ī�尡 ������ ���� ����
        {
            Reshuffle();
        }
        if (DrawPile.Count == 0) //���� �Ŀ��� ���� ī�尡 ������ null ��ȯ
        {
            return null;
        }
        CardData drawnCard = DrawPile[0];
        DrawPile.RemoveAt(0); //���� ���̿��� ����
        Hand.Add(drawnCard); //�ڵ� ���̿� �߰�
        return drawnCard; //���� ī�� ��ȯ
    }

    /// <summary>
    /// ī�带 ������ �Լ�
    /// </summary>
    /// <param name="card">���� ī��</param>
    public CardData DiscardCard(CardData card)
    {
        if (Hand.Contains(card))
        {
            Hand.Remove(card);
            DiscardPile.Add(card);
            return card;
        }
        else return null;
    }

    /// <summary>
    /// ȣ��� ī�带 ������ ���
    /// </summary>
    /// <param name="card">����� ī��</param>
    /// <param name="enemy">���õ� �� ��ü</param>
    public void PlayCard(CardData card, Enemy enemy)
    {
        if (Hand.Contains(card)) //�ڵ� ���̿� �ش� ī�尡 �����ϴ��� Ȯ��
        {
            if (player.Energy >= card.Cost)
            {
                player.Energy -= card.Cost;
                Hand.Remove(card);

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
        Debug.Log("Hand: " + Hand.Count);
    }
}
