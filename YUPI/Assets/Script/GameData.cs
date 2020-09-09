using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int money;

    public bool crown;
    public bool belt;

    public bool[] levelMoneyTaken;
    public bool[] boostPower;

    public GameData (GameManager gameManager)
    {
        level = gameManager.level;
        money = gameManager.money;

        crown = gameManager.crown;
        belt = gameManager.belt;

        for(int i=0; i <= gameManager.levelMoneyTaken.Length; i++)
            levelMoneyTaken[i] = gameManager.levelMoneyTaken[i];
        
        for (int i = 0; i <= gameManager.boostPower.Length; i++)
            boostPower[i] = gameManager.boostPower[i];
        
    }
}

