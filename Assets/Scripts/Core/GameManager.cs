using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;
    public Player Player => player;


    List<Enemy> enemies;



    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
    }

    //------------------------------------------------------------------------------------------
    //전투 요소

    private int turn;
    public int Turn
    {
        get => turn;
        set
        {
            turn = value;
            OnTurnChange?.Invoke(turn);
        }
    }
    Action<int> OnTurnChange;
    public void StartBattle()
    {
        turn = 0;
    }
    public void EndBattle()
    {

    }
    public void TurnStart()
    {
        Turn++;
    }
    public void TurnEnd() 
    {

    }
}
