using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject[] debris;
    public GameObject explosionPrefab;
    public Borders borders;
    public float initialHP;
    public uint points;
    public Cracking cracking;
    // Start is called before the first frame update
    private float HP;
    public Rigidbody2D rb;
    private bool isDebris = false;

    void Start()
    {
        //set random rotation speed and direction
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = Random.Range(-120f, 120f);

        //in case of big asteroid set random HP, damage and velocity otherwise parameters are passed through SetParameters() method
        if (!isDebris)
        {
            initialHP = Random.Range(20f, 35f);
            rb.velocity = new Vector2(Random.Range(-0.7f, 0.7f), Random.Range(-4f, -0.5f));
        }
        HP = initialHP;
        //the smallest debris is destroyed after random time
        if (debris.Length == 0)
        {
            float time = Random.Range(0.3f, 1.5f);
            Destroy(gameObject, time);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //correction of velocity in case of collision
        if(rb.velocity.y > -0.5f)
            rb.velocity = new Vector2(rb.velocity.x, -0.5f);
        if(rb.velocity.y < -4f)
            rb.velocity = new Vector2(rb.velocity.x, -4f);

        if (transform.position.x > borders.xMax || transform.position.y > borders.yMax || transform.position.x < borders.xMin || transform.position.y < borders.yMin)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().DoDamage(HP);
            SplitToDebris();
        }
    }
    // instantiate debris of asteroid
    private void SplitToDebris()
    {
        Vector3[] debPos = new Vector3[debris.Length];
        for(int i = 0; i< debris.Length; i++)
        {
            debPos[i] = new Vector3 (Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            for(int j = 0; j<i; j++)
            {
                if(Vector3.Distance(debPos[j], debPos[i]) <= 0.25f)
                {
                    debPos[i] = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                    j--;
                }
            }
        }
        for (int i = 0; i < debris.Length; i++)
        {
            GameObject deb = Instantiate(debris[i], transform.position + debPos[i], transform.rotation) as GameObject;
            deb.GetComponent<Asteroid>().SetParameters(Random.Range(10f, initialHP), new Vector2(rb.velocity.x + Random.Range(-0.5f, 0.5f), rb.velocity.y));
        }
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
    // method used to set parameters of debris
    public void SetParameters(float hp, Vector2 velocity)
    {
        this.isDebris = true;
        this.initialHP = hp;
        this.HP = this.initialHP;
        this.rb.velocity = velocity;
    }
    public void DoDamage(float damage)
    {
        HP -= damage;
        if (cracking != null)
        {
            float alpha = 1f - (HP / initialHP);
            cracking.SetAlpha(alpha);
        }
        if (HP <= 0)
        {
            LevelManager.Score += points;
            //Debug.Log(LevelManager.Score);
            SplitToDebris();
        }
    }
}
