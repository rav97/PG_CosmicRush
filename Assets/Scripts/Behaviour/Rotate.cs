using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        // rotate the gameobject at a given speed
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
