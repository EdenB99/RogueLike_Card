using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/Card", order = 0)]
public class CardBase : ScriptableObject
{
    public string CardName;
    public string CardDecription;
    public int CardCost;
    public Sprite CardImage;

    public virtual void Play(Player player, Enemy enemy)
    {

    }
}
