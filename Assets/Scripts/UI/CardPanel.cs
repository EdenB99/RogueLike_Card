using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPanel : MonoBehaviour
{
    public GameObject cardPrefab;
    public List<CardData> cards;

    private void Start()
    {
        cards = GameManager.Instance.Deck.Hand;
        GameManager.Instance.Deck.HandChange += UpdateHandUI;
    }
    private void UpdateHandUI(List<CardData> hand)
    {
        foreach (Transform Child in transform)
        {
            Debug.Log($"Destroy {Child} transform");
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
