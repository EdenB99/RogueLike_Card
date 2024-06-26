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
        }
    }

    private void UseCard(Card card)
    {
        Debug.Log("Player ��� ī��: " + card.cardData.name);
        //  ī�� ��� ȿ�� ���� �ϴ� ��
        Destroy(card.gameObject); // ī�� ������Ʈ ����, ī�� ������Ʈ Ǯ ����
    }
}