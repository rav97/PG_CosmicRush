using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Energy shield slowly fade out during time
public class ShieldTimeout : Timeout
{
    private Shooting playerShooting;
    private float remaining;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        exitTime = Time.time + duration;
        playerShooting = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //calculate remaining time, 0.3f used for rounding function
        remaining = 0.3f + exitTime - Time.time;
        // display remaining time to player
        playerShooting.SetShieldRemaining(Mathf.RoundToInt(remaining));
        // slowly fade out
        sr.color = new Color(1f, 1f, 1f, remaining/duration + 0.1f);
        // if time is up, it deactivate shield, restore default color and destroys
        if(Time.time > exitTime)
        {
            playerShooting.isShield = false;
            Destroy(gameObject);
        }
    }
}
