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



[CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/Card", order = 0)]
public class CardBase : ScriptableObject
{
    public string Name;
    public string Decription;
    public int Cost;
    public CardRarity Rarity;
    public CardType Type;
    public Sprite Image;

    public virtual void Play(Player player, Enemy enemy)
    {

    }
}
