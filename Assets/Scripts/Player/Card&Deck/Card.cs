using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    private CardData cardData;
    public CardData CardData
    {
        get => cardData;
        set
        {
            cardData = value;
            UpdateCard();
        }
    }

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
    private void UpdateCard()
    {
        if (CardData != null)
        {
            nameText.text = CardData.Name;
            descriptionText.text = CardData.Decription;
            costText.text = CardData.Cost.ToString();
            CardImage.sprite = CardData.Image;
        } else
        {
            nameText.text = null;
            descriptionText.text = null;
            costText.text = null;
            CostBackGround.sprite = null;
        }
    }
}
