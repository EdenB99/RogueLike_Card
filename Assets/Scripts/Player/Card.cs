using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardData cardData;

    TextMeshProUGUI nameText;
    Image CardImage;
    TextMeshProUGUI descriptionText;
    Image CostBackGround;
    TextMeshProUGUI costText;
    
    

    private void Awake()
    {
        nameText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        CardImage = transform.GetChild(1).GetComponent<Image>();
        descriptionText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        CostBackGround = transform.GetChild(3).GetComponent<Image>();
        costText = CostBackGround.GetComponentInChildren<TextMeshProUGUI>();
    }
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
        } else
        {
            nameText.text = null;
            descriptionText.text = null;
            costText.text = null;
            CostBackGround.sprite = null;
        }
    }
}
