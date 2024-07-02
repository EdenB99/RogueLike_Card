using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPanel : MonoBehaviour, IDropHandler
{
    DeckManager deck;
    private void Start()
    {
        deck = GameManager.Instance.Deck;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Card card = eventData.pointerDrag.GetComponent<Card>();
            if (card != null)
            {
                UseCard(card);
            }
            else
            {
                DragAndDrop dd = eventData.pointerDrag.GetComponent<DragAndDrop>();
                if (dd != null)
                {
                    dd.PositionReSet();
                }
            }
        }
    }

    private void UseCard(Card card)
    {   
        deck.PlayCard(card, GameManager.Instance.enemies[0]);
        //card.CardData.PlayCard(GameManager.Instance.enemies[0]); //리스트에서 첫번째 적에게 피해
        // 카드 오브젝트 제거, 카드 오브젝트 풀 구현
    }
    
}
