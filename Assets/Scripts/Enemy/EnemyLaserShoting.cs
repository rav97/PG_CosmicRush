using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserShoting : EnemyShooting
{
    public float firstDelay = 2.0f;
    public float laserDuration;
    // Start is called before the first frame update
    void Start()
    {
        nextFire = Time.time + firstDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire && LevelManager.PlayerAlive)
        {
            // makes sure that enemy is looking down when firing
            if (transform.eulerAngles.z == 180f)
            {
                GameObject laser = Instantiate(weapon, transform.position + (transform.up * 0.3f), transform.rotation);
                laser.transform.SetParent(transform); //this method allow to keep prefab scale
                laser.GetComponent<Timeout>().duration = laserDuration;
                nextFire = Time.time + fireSpeed;
            }
        }
    }
}
