using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private HealthBar healthBar;
    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        HP = InitialHP;
        healthBar = GameObject.Find("BossBar").GetComponent<HealthBar>();
        healthBar.SetHealthBar(1f);
        StartActing();
    }
    public new void DoDamage(float damage)
    {
        HP -= damage;
        float percent = (HP / InitialHP);
        healthBar.SetHealthBar(percent);

        if(cracking != null)
            cracking.SetAlpha(1f - percent);

        if (HP <= 0)
        {
            if(!isDead)
                StartCoroutine(Dying());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log(gameObject + " collided with Player");
            collision.gameObject.GetComponent<Player>().DoDamage(collisionDamage);
        }
    }
    private void StartActing()
    {
        foreach(Transform child in transform)
        {
            if(child.gameObject.GetComponent<EnemyShooting>() != null)
            {
                child.gameObject.GetComponent<EnemyShooting>().enabled = true;
            }
            if (child.gameObject.GetComponent<FollowPath>() != null)
            {
                child.gameObject.GetComponent<FollowPath>().enabled = true;
            }
        }
    }
    private void StopActing()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<EnemyShooting>() != null)
            {
                child.gameObject.GetComponent<EnemyShooting>().enabled = false;
            }
            if (child.gameObject.GetComponent<FollowPath>() != null)
            {
                child.gameObject.GetComponent<FollowPath>().enabled = false;
            }
        }
    }
    private IEnumerator Dying()
    {
        isDead = true;
        int l = Random.Range(3, 7);

        StopActing();

        //creates a few explosion
        for(int i = 0; i < l; i++)
        {
            Instantiate(explosionPrefab, transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1f, 1f)), transform.rotation);
            yield return new WaitForSeconds(0.4f);
        }

        Instantiate(explosionPrefab, transform.position, transform.rotation);
        LevelManager.Score += points;
        GameObject.Find("BossPanel").SetActive(false);
        MusicManager.Instance.PlayMusic(MusicManager.Music.Gameplay);
        Destroy(gameObject);
    }
}
