using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float InitialHP;
    public GameObject explosionPrefab;
    public float collisionDamage;
    public Cracking cracking;
    public uint points;

    protected float HP;
    // Start is called before the first frame update
    void Start()
    {
        HP = InitialHP;
    }
    public void DoDamage(float damage)
    {
        HP -= damage;
        if (cracking != null)
        {
            float alpha = 1f - (HP / InitialHP);
            cracking.SetAlpha(alpha);
        }
        //Debug.Log(gameObject + " - HP remaining: " + HP);
        if(HP <= 0)
        {
            LevelManager.Score += points;
            Explode();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Debug.Log(gameObject + " collided with Player");
            collision.gameObject.GetComponent<Player>().DoDamage(collisionDamage);
            Explode();
        }
    }
    protected void Explode()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
