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
    public void StartBattle()
    {

    }
    public void EndBattle()
    {

    }
}
