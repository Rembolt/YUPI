using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int level;
    public int money;

    public bool crown;
    public bool belt;

    public bool[] levelMoneyTaken;
    public bool[] boostPower;

    void Start()
    {
        levelMoneyTaken = new bool[100];
        boostPower = new bool[3];
    }

    void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    void LoadGame()
    {
        GameData data = SaveSystem.LoadGame();

        level = data.level;
        money = data.money;

        crown = data.crown;
        belt = data.belt;

        for (int i = 0; i <= data.levelMoneyTaken.Length; i++)
            levelMoneyTaken[i] = data.levelMoneyTaken[i];

        for (int i = 0; i <= data.boostPower.Length; i++)
            boostPower[i] = data.boostPower[i];
    }
}
