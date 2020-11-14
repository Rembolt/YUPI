using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public GameObject jail;
    public Image moneySac;
    private Color bright;
    private Color dark;
    public Text txt1, txt2;

    void Start()
    {
        bright = new Color(255, 255, 255);
        dark = new Color(0, 0, 0);
        string name = gameObject.name;
        string numbers = string.Empty;
        for (int i = 0; i < name.Length; i++)
        {
            if (char.IsDigit(name[i]))
                numbers += name[i];
        }
        int result = int.Parse(numbers);
        result++;
        txt1.text = result.ToString();
        txt2.text = result.ToString();
    }

    void Update()
    {
        string name = gameObject.name;
        string numbers = string.Empty;
        for (int i = 0; i < name.Length; i++)
        {
            if (char.IsDigit(name[i]))
                numbers += name[i];
        }
        int result = int.Parse(numbers);
        
        if (TheGameManager.instance.level >= result)
        {
            jail.SetActive(false);
            GetComponent<Button>().interactable = true;
        }
        else
        {
            jail.SetActive(true);
            GetComponent<Button>().interactable = false;
        }
            
        if (TheGameManager.instance.GetVariable(gameObject.name) == true)
        {
            moneySac.color = bright;
        }
        else
        {
            moneySac.color = dark;
        }
    }

    public void Play()
    {
        StartCoroutine(StartLevel());
    }
        
    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(2);
        TheGameManager.instance.anim.SetTrigger("Menu");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(gameObject.name);
    }
}
