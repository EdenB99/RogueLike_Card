using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card/AttackCard", order = 0)]
public class CardData_Attack : CardData
{
    public int AttackValue;
    public int AttackCount = 1;
    public bool isFullAttack = false;


   public override bool PlayCard(Enemy enemy)
    {
        Player player = GameManager.Instance.Player;
        for (int i = 0; i < AttackCount; i++) //공격횟수만큼
        {
            if (isFullAttack) //전체공격이면
            {
                List<Enemy> enemies = GameManager.Instance.enemies; //게임 매니저내 모든 적에게
                foreach (Enemy indexEnemy in enemies) //그 수만큼 피해
                {
                    indexEnemy.Health -= AttackValue + player.AdditionalDamage;
                }
            } else //전체공격이 아니면 대상 적에게만 피해
            {
                enemy.Health -= AttackValue+ player.AdditionalDamage;
            }
        }
        return true;
    }
}
