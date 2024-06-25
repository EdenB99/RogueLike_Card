using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardData cardData;

    public TextMeshPro nameText;
    public TextMeshPro descriptionText;
    public TextMeshPro costText;
    public SpriteRenderer CardImage;

    private void Start()
    {
        UpdateCard();
    }
    public void SetCardData(CardData newCardData)
    {
        cardData = newCardData;
        UpdateCard();
    }
    private void UpdateCard()
    {
        if (cardData != null)
        {
            nameText.text = cardData.Name;
            descriptionText.text = cardData.Decription;
            costText.text = cardData.Cost.ToString();
            CardImage.sprite = cardData.Image;
        }
    }
}
