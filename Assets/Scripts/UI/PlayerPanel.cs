using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerPanel : MonoBehaviour, IDropHandler
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
        Debug.Log("Player ��� ī��: " + card.CardData.name);
        //  ī�� ��� ȿ�� ���� �ϴ� ��
        Destroy(card.gameObject); // ī�� ������Ʈ ����, ī�� ������Ʈ Ǯ ����
    }
}