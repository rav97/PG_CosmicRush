using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public int bonusType;
    public float fallingSpeed = 5;
    public float acceleration;
    public int quantity;
    public Borders borders;
    /*
     * Bonus types:
     * 1 - HP restore
     * 2 - Shield
     * 3 - Special Weapon Laser
     * 4 - Special Weapon Nuke
     * 5 - Equip Plasma Bullet
     * 6 - Equip Triple Plasma Bullet
     * 7 - Equip Five Plasma Bullet
     * 8 - Equip Missles
     * 9 - Equip Blast
     */

    // Update is called once per frame
    void Update()
    {
        fallingSpeed += acceleration * Time.deltaTime;
        transform.position += transform.up * Time.deltaTime * fallingSpeed;

        //destroys if left the scene
        if (transform.position.x > borders.xMax || transform.position.y > borders.yMax || transform.position.x < borders.xMin || transform.position.y < borders.yMin)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;
            switch(bonusType)
            {
                case 1: {
                        player.GetComponent<Player>().AddHP(quantity);
                    };break;
                case 2:
                    {
                        player.GetComponent<Shooting>().EnableShield(quantity, GetComponent<SpriteRenderer>().sprite);
                    }; break;
                case 3:
                    {
                        player.GetComponent<Shooting>().SpecialWeapon(1, GetComponent<SpriteRenderer>().sprite);
                    }; break;
                case 4:
                    {
                        player.GetComponent<Shooting>().SpecialWeapon(0, GetComponent<SpriteRenderer>().sprite);
                    }; break;
                case 5:
                    {
                        player.GetComponent<Shooting>().ChangeWeapon(5, quantity, GetComponent<SpriteRenderer>().sprite);
                    }; break;
                case 6:
                    {
                        player.GetComponent<Shooting>().ChangeWeapon(1, quantity, GetComponent<SpriteRenderer>().sprite);
                    }; break;
                case 7:
                    {
                        player.GetComponent<Shooting>().ChangeWeapon(2, quantity, GetComponent<SpriteRenderer>().sprite);
                    }; break;
                case 8:
                    {
                        player.GetComponent<Shooting>().ChangeWeapon(3, quantity, GetComponent<SpriteRenderer>().sprite);
                    }; break;
                case 9:
                    {
                        player.GetComponent<Shooting>().ChangeWeapon(4, quantity, GetComponent<SpriteRenderer>().sprite);
                    }; break;
            }
            player.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
