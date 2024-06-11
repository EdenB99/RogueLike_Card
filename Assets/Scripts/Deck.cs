using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Deck : MonoBehaviour
{
    public List<CardBase> Allcards = new List<CardBase>();
    private List<CardBase> DrawPile;
    private List<CardBase> DiscardPile;
    private List<CardBase> Hand;


    private void Start()
    {
        DrawPile = new List<CardBase>(Allcards);
        DiscardPile = new List<CardBase>();
        Hand = new List<CardBase>();
        Shuffle(DrawPile);
    }
    /// <summary>
    /// 호출한 덱의 카드를 섞는 함수
    /// </summary>
    /// <param name="deck">섞어질 덱</param>
    public void Shuffle(List<CardBase> deck)
    {
        for (int i = 0; i < deck.Count; i++) //피셔 예이츠 셔플
        {
            CardBase tempCard = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = tempCard;
        }
    }
    /// <summary>
    /// 카드를 뽑고 뽑은 카드를 반환하는 함수
    /// </summary>
    /// <returns>뽑은 카드</returns>
    public CardBase DrawCard()
    {
        if (DrawPile.Count == 0) //뽑을 더미에 카드가 없으면 덱을 섞음
        {
            Reshuffle();
        }
        if (DrawPile.Count == 0) //섞은 후에도 뽑을 카드가 없으면 null 반환
        {
            return null;
        }
        CardBase drawnCard = DrawPile[0];
        DrawPile.RemoveAt(0); //뽑을 더미에서 제거
        Hand.Add(drawnCard); //핸드 더미에 추가
        return drawnCard; //뽑은 카드 반환
    }
    /// <summary>
    /// 뽑을 더미에 버려진 더미를 추가후 덱을 섞는 함수
    /// </summary>
    public void Reshuffle()
    {
        DrawPile.AddRange(DiscardPile);
        DiscardPile.Clear();
        Shuffle(DrawPile);
    }
    /// <summary>
    /// 호출된 카드를 덱에서 사용
    /// </summary>
    /// <param name="card">사용할 카드</param>
    /// <param name="enemy">선택된 적 개체</param>
    public void PlayCard(CardBase card, Enemy enemy)
    {
        if (Hand.Contains(card)) //핸드 더미에 해당 카드가 존재하는지 확인
        {
            DiscardCard(card);
            //카드 종류에 따라 조건문 추가필요
            

            card.Play(enemy); //카드 스크립트의 play 호출
        }
    }
    /// <summary>
    /// 카드를 버리는 함수
    /// </summary>
    /// <param name="card">버릴 카드</param>
    public void DiscardCard(CardBase card)
    {
        if (Hand.Contains(card))
        {
            Hand.Remove(card);
            DiscardPile.Add(card);
        }
    }
    /// <summary>
    /// 현재 덱 상황을 디버그로 송출
    /// </summary>
    public void PrintDeckStatus()
    {
        Debug.Log("Draw Pile: " + DrawPile.Count);
        Debug.Log("Discard Pile: " + DiscardPile.Count);
        Debug.Log("Hand: " + Hand.Count);
    }
}
