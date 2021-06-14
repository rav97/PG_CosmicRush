using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBall : Bullet
{
    public float stopPosMin, stopPosMax;
    public GameObject explosionPrefab;
    public float lifetime;

    private float stopPosition;
    private float deathTime;
    // Start is called before the first frame update
    void Start()
    {
        stopPosition = Random.Range(stopPosMin, stopPosMax);
        deathTime = Time.time + lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        // move bullet in direction it's facing
        speed += acceleration * Time.deltaTime;
        transform.position += transform.up * Time.deltaTime * speed;

        //destroy the object after leaving the scene
        if (transform.position.x > borders.xMax || transform.position.y > borders.yMax || transform.position.x < borders.xMin || transform.position.y < borders.yMin)
        {
            Destroy(gameObject);
        }

        // stops when it reaches position
        if (transform.position.y < stopPosition)
            speed = 0;

        // explode after a set time
        if(Time.time > deathTime)
        {
            Explode();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if touches player, it inflicts damage and explodes
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().DoDamage(damage);
            //Debug.Log("Took " + damage + " HP from Player");
            Explode();
        }
        if(collision.gameObject.tag == "SpecialWeapon")
        {
            Destroy(gameObject);
        }
    }
    // instantiate explosion prefabricate
    private void Explode()
    {
        Destroy(gameObject);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }
}
