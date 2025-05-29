using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    //these are the default values upon starting a new game

    public string PlayerName;

    public int BatteryCount;

    public Vector2 PlayerPosition;

    public bool gameInitialized;

    public GameData()
    {
        this.BatteryCount = 0;
        this.PlayerPosition = Vector2.zero;
        this.PlayerName = string.Empty;
        this.gameInitialized = true;
    }
}
