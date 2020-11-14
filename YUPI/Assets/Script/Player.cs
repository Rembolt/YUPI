using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Slider sliderXPosition;
    public Transform cameraRig;
    public float playerSpeedUp;
    public GameObject DeathFX;
    public Material Burned;
    public float life;/* Long term data */
    public Image []availableHearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private GameObject player;
    private Transform playerTransform;
    private Animator playerAnim;

    private bool moving;
    private bool isFalling;
    private bool dead;
    private Vector3 pos;
    private float fixedDeltaTime;
    private bool stoped = false;
    public float currentLife;
    private bool boosting;
    private Scene scene;

    private float moneySac;
    public Text sacsCollected1, sacsCollected2;
    public Image M1, M2, M3, M1e, M2e, M3e;           

    public static Player i;
    void Awake() { i = this; }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        playerAnim = player.GetComponent<Animator>();
        this.fixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = 1;
        moneySac = 0;
        currentLife = life;
        sliderXPosition.interactable = false;
        StartCoroutine(Starting());
        player.SetActive(false);

        for (int i = 0; i < availableHearts.Length; i++)
        {
            if (i < life)
                availableHearts[i].enabled = true;
            else
                availableHearts[i].enabled = false;

            if (i < currentLife)
                availableHearts[i].sprite = fullHeart;
            else
                availableHearts[i].sprite = emptyHeart;
        }
    }

    public void PlayerColision(Collider2D c)
    {
        if (c.gameObject.tag == "Spike Colliders")
            Dying();
        if (c.gameObject.tag == "Damageble")
            Damageble();              
        if (c.gameObject.tag == "Death")
            Dead();  
        if (c.gameObject.tag == "Boost")
            StartCoroutine(Boosted());
        if (c.gameObject.tag == "Heart")
        {
            if(currentLife < life)
                currentLife++;
            Destroy(c.gameObject);
        }        
        Color normal = new Color(255, 255, 255);
        if (c.gameObject.tag == "Money sac 1")
        {
            moneySac++;
            M1.color = normal;
            M1e.color = normal;
            Destroy(c.gameObject);
        }
        if (c.gameObject.tag == "Money sac 2")
        {
            moneySac++;
            M2.color = normal;
            M2e.color = normal;
            Destroy(c.gameObject);
        }
        if (c.gameObject.tag == "Money sac 3")
        {
            moneySac++;
            M3.color = normal;
            M3e.color = normal;
            Destroy(c.gameObject);
        }
        if (c.gameObject.tag == "EndGame")
        {
            StartCoroutine(EndGame());
        }       
    }

    public void Damageble()
    {
        currentLife--;
        if (currentLife <= 0)
            Dying();
        else
            StartCoroutine(Damaged());
    }

    
    void Update()
    {
      if(TheGameManager.gameIsPaused == false)
      {
            if (moving == true)
            {
                pos.x = sliderXPosition.value;
                playerTransform.position += new Vector3(pos.x - playerTransform.position.x * 0.3f, playerSpeedUp * Time.deltaTime, 0);
                cameraRig.position += new Vector3(0, playerSpeedUp * Time.deltaTime, 0);
                Time.timeScale = 1;
            }    
            else if (isFalling == true)
            {
                if(stoped == true) 
                {
                    Time.timeScale = 0.8f;
                }
                else if (Time.timeScale >= 0.3f)
                { Time.timeScale -= 0.01f; } 
                
            
                //Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
                playerTransform.position += new Vector3(pos.x - playerTransform.position.x * 0.3f, -playerSpeedUp * Time.deltaTime, 0);
            }

        if(dead == true)
        {
            Time.timeScale = 0.8f;
            sliderXPosition.interactable = false;
            stoped = true;
            moving = false;
            isFalling = false;
            playerTransform.position += new Vector3(pos.x - playerTransform.position.x * 0.3f, -playerSpeedUp * Time.deltaTime, 0);
        }

        for (int i = 0; i < availableHearts.Length; i++)
        {
            if (i < life)
                availableHearts[i].enabled = true;
            else
                availableHearts[i].enabled = false;
            
            if (i < currentLife)
                availableHearts[i].sprite = fullHeart;
            else
                availableHearts[i].sprite = emptyHeart;
        }

        if (boosting == true)
        {
            playerTransform.position += new Vector3(0, playerSpeedUp * Time.deltaTime * 4, 0);
        }
      }     
    }

    public void OnPointerDown(PointerEventData e)
    {   
        if (TheGameManager.gameIsPaused == false)
        {
           playerAnim.SetTrigger("Hiking");
           moving = true;
           isFalling = false;
           Debug.Log("Moving");
        } 
           
    }
    public void OnPointerUp(PointerEventData e)
    {
        if (TheGameManager.gameIsPaused == false)
        {
           playerAnim.SetTrigger("Falling");
           isFalling = true;
           moving = false;
           Debug.Log("Falling");
        }   
    }
    IEnumerator Starting()
    {
        yield return new WaitForSeconds(5.2f);
        stoped = false;
        sliderXPosition.interactable = true;
        TheGameManager.gameIsPaused = false;
        player.SetActive(true);
    }

    IEnumerator Damaged()
    {
        stoped = true;
        playerAnim.SetTrigger("Damaged");
        moving = false;
        isFalling = true;
        playerTransform.position += new Vector3(0, -0.2f, 0);
        sliderXPosition.interactable = false;  
        yield return new WaitForSeconds(1);
        sliderXPosition.interactable = true;
        stoped = false;
        moving = true;
        isFalling = false;
        playerAnim.SetTrigger("Hiking");
        Debug.Log("Recovered");
    }
    void Dying()
    {
        currentLife = 0;
        playerAnim.SetTrigger("Falling");
        stoped = true;
        moving = false;
        isFalling = true;
        sliderXPosition.interactable = false;
        dead = true;
        Debug.Log("Dying");
    }
    IEnumerator Boosted() 
    {
        if(dead != true)
        {
            boosting = true;
            playerAnim.SetTrigger("Boosting");
            yield return new WaitForSeconds(0.4f);
            playerAnim.SetTrigger("Hiking");
            boosting = false;
        }
       
    }
    IEnumerator EndGame()
    {
        Debug.Log("EndGame");
        TheGameManager.gameIsPaused = true;
        moving = false;
        isFalling = false;
        stoped = true;
        sliderXPosition.interactable = false;
        player.SetActive(false);
        TheGameManager.instance.LoadGame();
        string name = SceneManager.GetActiveScene().name;
        string numbers = string.Empty;
        for (int i = 0; i < name.Length; i++)
        {
            if (char.IsDigit(name[i]))
                numbers += name[i];
        }
        int result = int.Parse(numbers);
        if (TheGameManager.instance.level == result)
        {
            TheGameManager.instance.level++;
        }
        if (TheGameManager.instance.GetVariable(SceneManager.GetActiveScene().name) != true)
        {
            if (moneySac >= 3)
            {
                TheGameManager.instance.money++;
                TheGameManager.instance.SetVariable(SceneManager.GetActiveScene().name, true);
            }
        }
        TheGameManager.instance.SaveGame();
        Debug.Log("Updated");
        sacsCollected1.text = moneySac.ToString(); sacsCollected2.text = moneySac.ToString();
        TheGameManager.instance.startAnim.SetTrigger("End");
        TheGameManager.gameIsPaused = true;
        yield return new WaitForSeconds(2);
        TheGameManager.instance.anim.SetTrigger("End");
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("Levels");
    }
    private void Dead()
    {
        Instantiate(DeathFX, playerTransform.position, Quaternion.identity);
        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        playerSprite.material = Burned;
        playerAnim.SetTrigger("Dead");
        Debug.Log("Dead");
        TheGameManager.instance.Death();
    }
}
