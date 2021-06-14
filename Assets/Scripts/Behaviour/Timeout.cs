using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// destroys attached gameobject after set amount of time
public class Timeout : MonoBehaviour
{
    public float duration;
    protected float exitTime;

    void Start()
    {
        exitTime = Time.time + duration;
    }
    // Update is called once per frame
    void Update()
    {
        //it time is out destroys gameobject
        if(Time.time > exitTime)
        {
            Destroy(gameObject);
        }
    }
}