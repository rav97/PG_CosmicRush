using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float InitialHP;
    public GameObject explosionPrefab;
    public HealthBar healthBar;
    private float HP;
    // Start is called before the first frame update
    void Start()
    {
        HP = InitialHP;
        healthBar = GameObject.Find("Bar").GetComponent<HealthBar>();
    }
    public void DoDamage(float damage)
    {
        if (!GetComponent<Shooting>().isShield)
        {
            HP -= damage;
            //Debug.Log("Player - remaining HP: " + HP);
            healthBar.SetHealthBar(HP / InitialHP);
            if (HP <= 0)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
                LevelManager.PlayerAlive = false;
                Destroy(gameObject);
            }
        }
    }
    public void AddHP(float hp)
    {
        HP += hp;
        if (HP > InitialHP)
            HP = InitialHP;
        healthBar.SetHealthBar(HP / InitialHP);
        //Debug.Log("Player Healed");
    }
}
