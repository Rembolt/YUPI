using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public static Player i;
    void Awake() { i = this; }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        playerAnim = player.GetComponent<Animator>();
        this.fixedDeltaTime = Time.fixedDeltaTime;
        currentLife = life;
    }

    public void PlayerDamage(Collider2D c)
    {
        if (c.gameObject.tag == "Spike Colliders")
            StartCoroutine(Dying());
           
        if (c.gameObject.tag == "Damageble")
        {
            currentLife--;
            if(currentLife <= 0)
                StartCoroutine(Dying());
            else
                StartCoroutine(Damaged());
        }
            

        if (c.gameObject.tag == "Death")
            Dead();    
    }

    
    void Update()
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



    }

    public void OnPointerDown(PointerEventData e)
    {
        if (stoped == false)
        {
            playerAnim.SetTrigger("Hiking");
            moving = true;
            isFalling = false;
            Debug.Log("Moving");
        }
           
    }
    public void OnPointerUp(PointerEventData e)
    {
        if (stoped == false)
        {  
            playerAnim.SetTrigger("Falling");
            isFalling = true;
            moving = false;
            Debug.Log("Falling");
        }    
    }

    IEnumerator Damaged()
    {
        stoped = true;
        playerAnim.SetTrigger("Damaged");
        moving = false;
        isFalling = true;
        playerTransform.position += new Vector3(pos.x - playerTransform.position.x * 0.3f, -0.5f, 0);
        sliderXPosition.interactable = false;  
        yield return new WaitForSeconds(1);
        sliderXPosition.interactable = true;
        stoped = false;
        moving = true;
        isFalling = false;
        playerAnim.SetTrigger("Hiking");
        Debug.Log("Recovered");
    }

    IEnumerator Dying()
    {
        currentLife = 0;
        playerAnim.SetTrigger("Falling");
        stoped = true;
        moving = false;
        isFalling = true;
        sliderXPosition.interactable = false;
        dead = true;
        Debug.Log("Dying");
        yield return "Dying";
    }

    void Dead()
    {
        Instantiate(DeathFX, playerTransform.position, Quaternion.identity);
        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        playerSprite.material = Burned;
        playerAnim.SetTrigger("Dead");
        Debug.Log("Dead");
    }
}
