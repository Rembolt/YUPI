using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    public GameObject arrow;
    public float timeBetweenShots;
    public Transform arrowStartingPoint;
    private float timer;

    void Start()
    {
        timer = 0;
    }
    void Update()
    {
        if(timer <= 0)
        {
            Instantiate(arrow, arrowStartingPoint.position, transform.rotation);
            timer = timeBetweenShots;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
