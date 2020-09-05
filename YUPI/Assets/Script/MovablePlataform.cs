using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlataform : MonoBehaviour
{
    public Transform[] targets;
    public float speed;
    private Vector3 nextTarget;
    private int i;

    void Start()
    {
        i = 0;
        nextTarget = targets[i].position;
    }
    void Update()
    {
        if (i < targets.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextTarget, speed * Time.deltaTime);
            if (transform.position == nextTarget)
            {
                nextTarget = targets[i].position;
                i++;
            }       
        }
        else
        {
            i = 0;
        }
            
    }
}
