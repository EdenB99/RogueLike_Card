using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DefenceType : byte
{
    Heal = 0,
    Armor,

}


[CreateAssetMenu(fileName = "NewCard", menuName = "Card/DefenceCard", order = 1)]
public class CardData_Defence : CardData
{
    public int defenceValue;
    public int defenceCount = 1;
    public DefenceType defenceType;


    public override bool PlayCard()
    {
        for (int i = 0;  i < defenceCount; i++)
        {
            Player player = GameManager.Instance.Player;
            switch (defenceType)
            {
                case DefenceType.Heal:
                    player.Health += defenceValue;
                    break;
                case DefenceType.Armor:
                    player.Armor += defenceValue;
                    break;
                default:
                    break;
            }
        }
        return true;
    }
}
