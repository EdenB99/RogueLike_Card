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
            if (value <= 0) //변경되는 값이 1보다 작다면
            {
                int LeftDamge = value - armor; //현재 아머값을 넘긴만큼을 델리게이트로 호출
                OnArmorBreak?.Invoke(LeftDamge);
                armor = 0;  //그후 아머값을 0으로 변경
            }
            else //변경되는 값이 그보다 크면 그대로 변경
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
        // 적 사망 처리
    }

    public void PerformAction(Player player)
    {
        // 적의 행동 로직
    }
}
