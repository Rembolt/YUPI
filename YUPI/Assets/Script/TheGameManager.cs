using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheGameManager : MonoBehaviour
{
    public int level;
    public int money;

    public bool crown;
    public bool belt;
    public int boostPower;

    public bool l0, l1, l2, l3, l4, l5, l6, l7, l8, l9, l10, l11;

    public static bool gameIsPaused;
    public Animator anim;
    public Animator startAnim;
    public Animator playerStartAnim;
    public GameObject pauseUI;
    public GameObject deathUI;
    public GameObject pauseButton;
    public static TheGameManager instance;
    public Text t1, t2;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        
        gameIsPaused = true;
        LoadGame();
    }
    void Start()
    {
        startAnim.SetTrigger("Start");
        playerStartAnim.SetTrigger("Start");
        Time.timeScale = 1;
    }
    void Update()
    {
        if (Player.i != null)
        {
            if (Player.i.currentLife <= 0)
            {
                pauseButton.GetComponent<Button>().interactable = false;
            }
        }

        if(SceneManager.GetActiveScene().name == "Menu")
            t1.text = money.ToString();t2.text = money.ToString();

        Debug.Log("Level = " + level);
        Debug.Log("Money = " + money);
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        pauseUI.SetActive(true);
        Time.timeScale = 1;
        anim.SetTrigger("Pause");
        StartCoroutine(Delay(pauseUI,true, 0, 0.8f));
    }

    IEnumerator Delay(GameObject UI,bool goBy, float s, float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = s;
        UI.SetActive(goBy);
        gameIsPaused = goBy;
    }       
    public void UnPause()
    {
        gameIsPaused = true;
        Time.timeScale = 1;
        anim.SetTrigger("UnPause");
        StartCoroutine(Delay(pauseUI, false, 1, 0.8f));       
    }

    public void Retry()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void Menu()
    {
        gameIsPaused = true;
        Time.timeScale = 1f;
        anim.SetTrigger("Menu");
        StartCoroutine(LoadAScene("Menu", 0.5f));
    }

    public void Settings()
    {
        gameIsPaused = true;
        Time.timeScale = 1f;
        anim.SetTrigger("Menu");
        StartCoroutine(LoadAScene("Settings", 0.5f));
    }

    IEnumerator LoadAScene(string scene, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
        Debug.Log(scene + " Loaded");
    }         

    public void Death()
    {
        gameIsPaused = true;
        Time.timeScale = 1f;
        deathUI.SetActive(true);
        anim.SetTrigger("Death");
        StartCoroutine(Delay(deathUI, true, 0.5f, 0.8f));
    }

    public bool GetVariable(string name)
    {       
        return (bool)this.GetType().GetField(name).GetValue(this);        
    }
    public void SetVariable(string name, bool value)
    {
        this.GetType().GetField(name).SetValue(this, value);
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
        Debug.Log("Saved");
    }

    public void LoadGame()
    {
        GameData data = SaveSystem.LoadGame();

        level = data.level;
        money = data.money;
        boostPower = data.boostPower;

        crown = data.crown;
        belt = data.belt;

        l0 = data.l0;
        l1 = data.l1;
        l2 = data.l2;
        l3 = data.l3;
        l4 = data.l4;
        l5 = data.l5;
        l6 = data.l6;
        l7 = data.l7;
        l8 = data.l8;
        l9 = data.l9;
        l10 = data.l10;
        l11= data.l11;
        Debug.Log("Loaded");
    }

    public void Backto0()
    {
        GameData data = SaveSystem.LoadGame();

        level = 0;
        money = 0;
        boostPower = 0;

        crown = false;
        belt = false;

        l0 = false;
        l1 = false;
        l2 = false;
        l3 = false;
        l4 = false;
        l5 = false; 
        l6 = false;
        l7 = false;
        l8 = false;
        l9 = false;
        l10 = false;
        l11 = false;
        SaveSystem.SaveGame(this);
        Debug.Log("Everything back to zero");
    }
}
