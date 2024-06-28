using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPanel : MonoBehaviour
{
    public GameObject cardPrefab;

    public void UpdateHandUI(List<CardData> hand)
    {
        foreach (Transform Child in transform)
        {
            Destroy(Child.gameObject);
        }
        foreach (CardData cardData in hand)
        {
            GameObject cardObject = Instantiate(cardPrefab, transform);
            Card card = cardObject.GetComponent<Card>();
            card.CardData = cardData;
        }
    }
}
