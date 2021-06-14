using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberShooting : EnemyShooting
{
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            //set player position as target
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch (System.NullReferenceException ex)
        {
            Debug.LogWarning(ex);
            target = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // if gameobject is near the X position of target it drops the bomb
            if (Mathf.Abs(transform.position.x - target.position.x) <= 0.4f && Time.time > nextFire)
            {
                Instantiate(weapon, transform.position + (transform.up / 1.5f), transform.rotation);
                nextFire = Time.time + fireSpeed;
            }
        }
        else
        {
            // if target is not found (because of some error), bomber behave as regular enemy
            if (Time.time > nextFire && LevelManager.PlayerAlive)
            {
                // makes sure that enemy is firing towards the player
                // uses dot product to calculate vector between direction that gameobject is facing and down direction
                if (Vector3.Dot(transform.up, Vector3.down) > 0.2)
                {
                    Instantiate(weapon, transform.position + (transform.up / 1.5f), transform.rotation);
                    nextFire = Time.time + fireSpeed;
                }
            }
        }
    }
}
