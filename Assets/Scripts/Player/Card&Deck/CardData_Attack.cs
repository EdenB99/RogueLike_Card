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
        Debug.Log("Attack");
        Player player = GameManager.Instance.Player;
        for (int i = 0; i < AttackCount; i++) //����Ƚ����ŭ
        {
            if (isFullAttack) //��ü�����̸�
            {
                List<Enemy> enemies = GameManager.Instance.enemies; //���� �Ŵ����� ��� ������
                foreach (Enemy indexEnemy in enemies) //�� ����ŭ ����
                {
                    indexEnemy.Health -= AttackValue + player.AdditionalDamage;
                }
            } else //��ü������ �ƴϸ� ��� �����Ը� ����
            {
                Debug.Log($"Deal {AttackValue} to {enemy}");
                enemy.Health -= AttackValue+ player.AdditionalDamage;
            }
        }
        return true;
    }
}
