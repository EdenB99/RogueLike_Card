using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card/TechniqueCard", order = 2)]
public class CardData_Technique : CardData
{
    public int DrawValue;


    public override bool PlayCard()
    {
        DeckManager Deck = GameManager.Instance.Deck;
        for (int i = 0; i < DrawValue; i++)
        {
            Deck.DrawCard();
        }
        return true;
    }
    public override bool PlayCard(Enemy enemy)
    {
        DeckManager Deck = GameManager.Instance.Deck;
        for (int i = 0; i < DrawValue; i++)
        {
            Deck.DrawCard();
        }
        return true;
    }
}
