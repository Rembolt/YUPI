using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInScript : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D c)
    {
        Debug.Log("Hitted");
        Player.i.PlayerColision(c);
    }
}
