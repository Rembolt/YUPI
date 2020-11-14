using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    void Start()
    {

    }

   
    void Update()
    {
       Debug.Log("Level: " + TheGameManager.instance.level);
       Debug.Log("Completed: " + TheGameManager.instance.GetVariable(SceneManager.GetActiveScene().name));
       Debug.Log("Money: " + TheGameManager.instance.money);
    }

    public void Do()
    {
        int sac = 3;

        char sceneLetter = SceneManager.GetActiveScene().name[1];
        int result = 0; result = 10 * result + (sceneLetter - 48);
        if (TheGameManager.instance.level < result)
        {
            TheGameManager.instance.level++;
        }
        if (TheGameManager.instance.GetVariable(SceneManager.GetActiveScene().name) != true)
        {
            if (sac >= 3)
            {
                TheGameManager.instance.money++;
                TheGameManager.instance.SetVariable(SceneManager.GetActiveScene().name, true);
            }
        }
    }

    public void Load()
    {
        TheGameManager.instance.LoadGame();
    }
    public void Save()
    {
        TheGameManager.instance.SaveGame();
    }
}
