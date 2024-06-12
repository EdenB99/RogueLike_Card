using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Deck : MonoBehaviour
{
    //�� ���� ���

    public List<CardBase> Allcards = new List<CardBase>();
    private List<CardBase> DrawPile;
    private List<CardBase> DiscardPile;
    private List<CardBase> Hand;

    /// <summary>
    /// �ִ� �ڵ� ����
    /// </summary>
    private const int MaxHandSize = 10;

    Player player;

    private void Start()
    {
        DrawPile = new List<CardBase>(Allcards);
        DiscardPile = new List<CardBase>();
        Hand = new List<CardBase>();
        Shuffle(DrawPile);
        player = GameManager.Instance.Player;
    }



    //----------------------------------------------------------------------------------------------------
    //�� ��ȣ�ۿ� �Լ�


    /// <summary>
    /// ȣ���� ���� ī�带 ���� �Լ�
    /// </summary>
    /// <param name="deck">������ ��</param>
    public void Shuffle(List<CardBase> deck)
    {
        for (int i = 0; i < deck.Count; i++) //�Ǽ� ������ ����
        {
            CardBase tempCard = deck[i];
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
    public CardBase DrawCard()
    {
        if (Hand.Count >= MaxHandSize)
        {
            Debug.Log("Hand is full.");
            CardBase DisCard = DrawPile[0];
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
        CardBase drawnCard = DrawPile[0];
        DrawPile.RemoveAt(0); //���� ���̿��� ����
        Hand.Add(drawnCard); //�ڵ� ���̿� �߰�
        return drawnCard; //���� ī�� ��ȯ
    }

    /// <summary>
    /// ī�带 ������ �Լ�
    /// </summary>
    /// <param name="card">���� ī��</param>
    public CardBase DiscardCard(CardBase card)
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
    public void PlayCard(CardBase card, Enemy enemy)
    {
        if (Hand.Contains(card)) //�ڵ� ���̿� �ش� ī�尡 �����ϴ��� Ȯ��
        {
            if (player.Energy >= card.Cost)
            {
                player.Energy -= card.Cost;
                Hand.Remove(card);

                //ī�� ������ ���� ���ǹ� �߰��ʿ� *�Ҹ� ���� Ű����
                DiscardPile.Add(card);
                card.Play(enemy);
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
