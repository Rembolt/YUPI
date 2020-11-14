using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu: MonoBehaviour
{ 

    public static bool gameIsPaused;
    public Animator anim;
    public Animator playerAnim;
    public static Menu instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        
        gameIsPaused = false;
    }
    void Start()
    {
        anim.SetTrigger("Menu Start");
        StartCoroutine(RunAnim());
        playerAnim.SetTrigger("Idle");
    }
    IEnumerator RunAnim()
    {
        yield return new WaitForSeconds(7);
        anim.SetTrigger("Menu Idle"); 
    }  

    IEnumerator PlayGame(string scene)
    {
        yield return new WaitForSeconds(1.8f);
        anim.SetTrigger("Menu");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(LoadAScene(scene, 3));
    }

    public void Settings()
    {
        playerAnim.SetTrigger("Change Scene");
        Interactble(false);
        StartCoroutine(PlayGame("Settings"));
    }
    public void Play()
    {
        playerAnim.SetTrigger("Change Scene");
        StartCoroutine(PlayGame("Levels"));
        Interactble(false);
    }
    public void Upgrades()
    {
        playerAnim.SetTrigger("Change Scene");
        StartCoroutine(PlayGame("Upgrades"));
        Interactble(false);
    }
    public void Tutorial()
    {
        playerAnim.SetTrigger("Change Scene");
        StartCoroutine(PlayGame("Tutorial"));
        Interactble(false);
    }
    public void Quit()
    {
        anim.SetTrigger("Menu");
        StartCoroutine(Quiting());
        Interactble(false);
    }
    IEnumerator Quiting()
    {
        yield return new WaitForSeconds(0.8f);
        Application.Quit();
        Debug.Log("Quited from Application");
    }
    void Interactble(bool Bool)
    {
        Button[] buttons = gameObject.GetComponentsInChildren<Button>();
        for(int i = 0; i < buttons.Length; i++)
            buttons[i].interactable = Bool;
    }
    IEnumerator LoadAScene(string scene, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
        Debug.Log(scene + " Loaded");
    }
}
