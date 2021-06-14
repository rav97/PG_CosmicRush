using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject[] bullet_prefab;
    public GameObject[] specialWeaponPrefab;
    public float damageBonus;

    [HideInInspector]
    public bool isShield;
    [HideInInspector]
    public bool ShieldEnabled;

    private int currentWeapon = 0;
    private int currentSpecialWeapon = -1;
    private float nextFire = 0f;
    private float weaponSpeed;
    private int ammo = 0;
    private int specialCount = 0;
    private Frame[] frame = new Frame[3];
    
    private void Awake()
    {
        frame[0] = GameObject.Find("PrimaryFrame").GetComponent<Frame>();
        frame[1] = GameObject.Find("SpecialFrame").GetComponent<Frame>();
        frame[2] = GameObject.Find("BonusFrame").GetComponent<Frame>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isShield = false;
        ShieldEnabled = false;
        // firing speed depends on the type od weapon
        weaponSpeed = bullet_prefab[currentWeapon].GetComponent<Bullet>().fireSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Fire1") != 0 && Time.time > nextFire)
        {
            GameObject bullet = Instantiate(bullet_prefab[currentWeapon], transform.position + new Vector3(0f, 0.6f), transform.rotation) as GameObject;
            bullet.GetComponent<Bullet>().damage *= damageBonus;
            nextFire = Time.time + weaponSpeed;

            if (currentWeapon != 0)
            {
                ammo--;
                frame[0].SetCount(ammo);

                if (ammo <= 0)
                {
                    ChangeWeapon(0, 0, null);
                    frame[0].SetCount(0);
                }
            }
        }
        if (Input.GetButtonDown("Fire2") && specialCount > 0)
        {
            if (currentSpecialWeapon == 0)
            {
                Instantiate(specialWeaponPrefab[currentSpecialWeapon], new Vector3(0, 0), transform.rotation);
                GameObject shield = Instantiate(specialWeaponPrefab[2], new Vector3(transform.position.x, transform.position.y), transform.rotation, transform);
                shield.GetComponent<ShieldTimeout>().enabled = false;
                Destroy(shield, 1.8f);
            }
            else
            {
                GameObject laser = Instantiate(specialWeaponPrefab[currentSpecialWeapon], new Vector3(transform.position.x, transform.position.y + 0.38f), transform.rotation) as GameObject;
                laser.transform.SetParent(transform);
            }
            specialCount--;
            frame[1].SetCount(specialCount);
            if(specialCount == 0)
            {
                currentSpecialWeapon = -1;
            }
            //Debug.Log("Special Weapon spawned ");
        }
        if (ShieldEnabled)
        {
            frame[2].frameImage.color = Color.white;
            if (Input.GetButtonDown("Shield"))
            {
                isShield = true;
                Instantiate(specialWeaponPrefab[2], new Vector3(transform.position.x, transform.position.y), transform.rotation, transform);
                ShieldEnabled = false;
            }
        }
    }
    public void ChangeWeapon(int weaponID, int count, Sprite image)
    {
        if(weaponID == currentWeapon)
        {
            ammo += count;
            frame[0].SetCount(ammo);
        }
        else
        {
            currentWeapon = weaponID;
            ammo = count;
            frame[0].SetCount(ammo);
            weaponSpeed = bullet_prefab[currentWeapon].GetComponent<Bullet>().fireSpeed;
            frame[0].SetImage(image);
        }
    }
    public void SpecialWeapon(int weaponID, Sprite image)
    {
        if (weaponID == currentSpecialWeapon)
        {
            specialCount++;
            frame[1].SetCount(specialCount);
        }
        else
        {
            currentSpecialWeapon = weaponID;
            specialCount = 1;
            frame[1].SetCount(specialCount);
            frame[1].SetImage(image);
        }
    }
    public void EnableShield(float time, Sprite image)
    {
        ShieldEnabled = true;
        specialWeaponPrefab[2].GetComponent<ShieldTimeout>().duration = time;
        frame[2].SetImage(image);
    }
    public void SetShieldRemaining(int time)
    {
        frame[2].SetCount(time);
    }
}
