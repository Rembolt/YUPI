using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public GameObject destructionFX;

    void OnTriggerEnter2D (Collider2D c)
    {
        if(c.gameObject.tag == "Player")
            Destruction();
        if(c.gameObject.tag == "Arrow Launcher")
            Destruction();
    }

    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }
    void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
