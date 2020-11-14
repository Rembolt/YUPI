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
    public int boostPower;

    public bool l0, l1, l2, l3, l4, l5, l6, l7, l8, l9, l10, l11;

    public GameData (TheGameManager gameManager)
    {

        level = gameManager.level;
        money = gameManager.money;

        boostPower = gameManager.boostPower;
        crown = gameManager.crown;
        belt = gameManager.belt;

        l0 = gameManager.l0;
        l1 = gameManager.l1;
        l2 = gameManager.l2;
        l3 = gameManager.l3;
        l4 = gameManager.l4;
        l5 = gameManager.l5;
        l6 = gameManager.l6;
        l7 = gameManager.l7;
        l8 = gameManager.l8;
        l9 = gameManager.l9;
        l10 = gameManager.l10;
        l11 = gameManager.l11;
    }
}

