using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static string playerName = "Player";
    public static GameRank gameRank = new GameRank(Application.persistentDataPath + "/" + "GameRank.gr");
    public static int shipType = 0;
    public static void SetPlayerName(string name)
    {
        playerName = name;
    }
}
