using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public GameObject Laserbeam;
    // Start is called before the first frame update
    void Start()
    {
        Laserbeam.SetActive(true);
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0f, transform.up.y * 0.35f), transform.up);
        //Debug.Log(transform.up);
        //Debug.Log(transform.up * 0.35f);
        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.y - transform.position.y) * 0.87f;
            //Debug.Log(distance);
            transform.localScale = new Vector3(1f, distance, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 8f, 1f);
        }
    }
}
