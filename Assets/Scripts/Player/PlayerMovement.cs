using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Borders borders;

    // Update is called once per frame
    void Update()
    {
        // get axis to move and create velocity vector
        float vertical = Input.GetAxis("Vertical") * speed;
        float horizontal = Input.GetAxis("Horizontal") * speed;

        // move player in the direction and distance of translation 1 unit/second
        transform.Translate(horizontal * Time.deltaTime, vertical * Time.deltaTime,  0);

        // move player to the bottom of scene
        if (vertical == 0 && transform.position.y > borders.yMin + 1.0f)
            transform.Translate(0, -3.0f * Time.deltaTime, 0);

        // prevents the player from leaving the scene
        if (transform.position.x > borders.xMax)
            transform.position = new Vector3(borders.xMax, transform.position.y);
        if (transform.position.x < borders.xMin)
            transform.position = new Vector3(borders.xMin, transform.position.y);
        if (transform.position.y > borders.yMax)
            transform.position = new Vector3(transform.position.x, borders.yMax);
        if (transform.position.y < borders.yMin)
            transform.position = new Vector3(transform.position.x, borders.yMin);
    }
}
