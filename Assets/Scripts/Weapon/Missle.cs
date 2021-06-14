using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public GameObject explosionPrefab;

    // if the rocket hits an object with different tag it explodes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && gameObject.tag == "PlayerWeapon")
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
        if (collision.gameObject.tag == "Asteroid" && gameObject.tag == "PlayerWeapon")
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "EnemyWeapon")
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
        if (collision.gameObject.tag == "Boss" && gameObject.tag == "PlayerWeapon")
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
    }
}
