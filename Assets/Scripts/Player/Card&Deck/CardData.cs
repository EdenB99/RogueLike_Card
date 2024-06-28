using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardRarity: byte
{
    Nomal = 0,
    Rare,
    Unique,
    Epic,
}
public enum CardType: byte
{
    Attack = 0,
    defence,
    posture,
    technique,
}




public class CardData : ScriptableObject
{
    public string Name;
    public string Decription;
    public int Cost;
    public CardRarity Rarity;
    public CardType Type;
    public Sprite Image;

    public virtual bool PlayCard()
    {
        return false;
    }
    public virtual bool PlayCard(Enemy enemy)
    {
        return false;
    }
   
}
