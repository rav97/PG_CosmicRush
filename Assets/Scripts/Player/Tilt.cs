using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilt : MonoBehaviour
{
    private Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
        anim["PlayerTilt"].speed = 4f;
        anim["PlayerUntilt"].speed = 4f;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0f)
        {
            if(transform.rotation.eulerAngles.y <= 15f)
                anim.Play("PlayerTilt");
        }
        if(Input.GetButtonUp("Horizontal"))
        {
            if(Input.GetAxis("Horizontal") == 0)
                anim.Play("PlayerUntilt");
        }
    }
}
