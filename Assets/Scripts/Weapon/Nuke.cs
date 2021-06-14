using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : MonoBehaviour
{
    public float damage;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().DoDamage(damage);
        }
        if (collision.gameObject.tag == "Asteroid")
        {
            collision.gameObject.GetComponent<Asteroid>().DoDamage(damage);
        }
        if(collision.gameObject.tag == "EnemyWeapon")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<Boss>().DoDamage(damage);
        }
    }
}
