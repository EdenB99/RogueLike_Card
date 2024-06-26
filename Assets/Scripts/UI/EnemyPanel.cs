using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPanel : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Card card = eventData.pointerDrag.GetComponent<Card>();
            if (card != null)
            {
                UseCard(card);
            }
        }
    }

    private void UseCard(Card card)
    {
        Debug.Log("Enemy 사용 카드: " + card.cardData.name);
        //  카드 사용 효과 구현 하는 곳
        Destroy(card.gameObject); // 카드 오브젝트 제거, 카드 오브젝트 풀 구현
    }
}
