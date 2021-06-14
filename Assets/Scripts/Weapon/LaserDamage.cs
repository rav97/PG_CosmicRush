using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    private GameObject hit;
    private bool isCollision = false;
    private void FixedUpdate()
    {
        if (isCollision)
        {
            if (hit != null)
            {
                if (hit.tag == "Enemy")
                {
                    hit.GetComponent<Enemy>().DoDamage(damage * Time.deltaTime);
                }
                if (hit.tag == "Asteroid")
                {
                    hit.GetComponent<Asteroid>().DoDamage(damage * Time.deltaTime);
                }
                if (hit.tag == "Boss")
                {
                    hit.GetComponent<Boss>().DoDamage(damage / 5 * Time.deltaTime);
                }
                if (hit.tag == "Player")
                {
                    hit.GetComponent<Player>().DoDamage(damage / 10 * Time.deltaTime);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" ||
            collision.gameObject.tag == "Enemy" ||
            collision.gameObject.tag == "Boss" ||
            collision.gameObject.tag == "Asteroid")
        {
            isCollision = true;
            hit = collision.gameObject;
            //Debug.Log("Entered: " + hit);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" ||
            collision.gameObject.tag == "Enemy" ||
            collision.gameObject.tag == "Boss" ||
            collision.gameObject.tag == "Asteroid")
        {
            isCollision = false;
            //Debug.Log("Exited: " + collision.gameObject);
        }
    }
}
