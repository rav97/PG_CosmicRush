using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject weapon;
    public float fireSpeed;

    protected float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        nextFire = Time.time + 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if player is alive it shoots every fireSpeed interval
        if (Time.time > nextFire && LevelManager.PlayerAlive)
        {
            // makes sure that enemy is firing towards the player
            // uses dot product to calculate vector between direction that gameobject is facing and down direction
            if (Vector3.Dot(transform.up, Vector3.down) > 0.2)
            {
                Instantiate(weapon, transform.position + (transform.up / 1.2f), transform.rotation);
                nextFire = Time.time + fireSpeed;
            }
        }
    }
}
