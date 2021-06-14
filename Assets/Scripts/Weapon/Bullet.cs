using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isMultiple;
    public float speed;
    public float fireSpeed;
    public float acceleration;
    public float damage;
    public Borders borders;
    // Start is called before the first frame update
    void Start()
    {
        // gives the bullet tag depending of the direction its facing on create, player firing up, enemy firing down
        if (gameObject.tag == "Weapon")
        {
            if (Vector3.Dot(transform.up, Vector3.down) < 0)
                gameObject.tag = "PlayerWeapon";
            else
            {
                gameObject.tag = "EnemyWeapon";
                /*GetComponent<Bullet>().damage *= 0.75f;
                GetComponent<Bullet>().speed *= 0.9f;*/
            }
        }
        if(transform.childCount > 0 && isMultiple)
        {
            foreach(Transform child in transform)
            {
                child.GetComponent<Bullet>().damage = this.damage;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // move bullet in direction it's facing
        speed += acceleration * Time.deltaTime;
        transform.position += transform.up * Time.deltaTime * speed;

        //destroys if left the scene
        if (transform.position.x > borders.xMax || transform.position.y > borders.yMax || transform.position.x < borders.xMin || transform.position.y < borders.yMin)
        {
            Destroy(gameObject);
        }
        //or after 2 seconds
        Destroy(gameObject, 10f);
        
        if(isMultiple)
        {
            if (transform.childCount <= 0)
                Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // "friendly fire" is unsupported
        if(collision.gameObject.tag == "Enemy" && gameObject.tag == "PlayerWeapon")
        {
            collision.gameObject.GetComponent<Enemy>().DoDamage(damage);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Asteroid" && gameObject.tag == "PlayerWeapon")
        {
            collision.gameObject.GetComponent<Asteroid>().DoDamage(damage);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Player" && gameObject.tag == "EnemyWeapon")
        {
            collision.gameObject.GetComponent<Player>().DoDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Boss" && gameObject.tag == "PlayerWeapon")
        {
            collision.gameObject.GetComponent<Boss>().DoDamage(damage);
            Destroy(gameObject);
        }
        // special weapon destroys bullet
        if (collision.gameObject.tag == "SpecialWeapon" && gameObject.tag == "EnemyWeapon")
        {
            Destroy(gameObject);
        }
    }
}
