using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private int health;
    public int Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
            {
                health = 0;
                Die();
            }
        }
    }
    private int armor;
    public int Armor
    {
        get => armor;
        set
        {
            if (value <= 0) //����Ǵ� ���� 1���� �۴ٸ�
            {
                int LeftDamge = value - armor; //���� �ƸӰ��� �ѱ丸ŭ�� ��������Ʈ�� ȣ��
                OnArmorBreak?.Invoke(LeftDamge);
                armor = 0;  //���� �ƸӰ��� 0���� ����
            }
            else //����Ǵ� ���� �׺��� ũ�� �״�� ����
            {
                armor = value;
            }
        }
    }
    Action<int> OnArmorBreak;

    public int AdditionalDamage;

    private void Awake()
    {
        OnArmorBreak += (leftDamage) => Health -= leftDamage;
    }
    private void Die()
    {
        // �� ��� ó��
    }

    public void PerformAction(Player player)
    {
        // ���� �ൿ ����
    }
}
