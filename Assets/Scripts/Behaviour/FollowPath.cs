using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    public float stopTime;
    public bool circle;
    public bool StopAtEveryPoint;
    public bool facePoints;

    private int i;
    private float waitUntil;
    private Vector3[] coordinates;
    private int dir = 1;

    // Start is called before the first frame update
    void Start()
    {
        coordinates = new Vector3[points.Length];
        i = 0;
        for(int i = 0; i < points.Length; i++)
        {
            coordinates[i] = points[i].position;
            Destroy(points[i].gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rotates the object so that it looks in the direction of movement
        if (facePoints)
        {
            Vector3 dif = coordinates[i] - transform.position;
            float rotationZ = -Mathf.Atan2(dif.x, dif.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                Quaternion.Euler(0.0f, 0.0f, rotationZ), Time.deltaTime * 5);
        }
        //moves towards next position if has waited a certain amount of time
        if (Time.time > waitUntil)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                coordinates[i], moveSpeed * Time.deltaTime);
        }
        // when reached position
        if (Vector3.Distance(transform.position, coordinates[i]) < 0.01f)
        {
            //stops at every point
            if (StopAtEveryPoint)
                waitUntil = Time.time + stopTime;
            else
            {
                //stops on first and last point
                if (i == 0 || i == points.Length - 1)
                    waitUntil = Time.time + stopTime;
            }
            // increment or decrement depending on the direction
            i += dir;

            if (circle)
            {
                // move sequentially on all points in a circle
                // 1->2->3->4->1->2...
                if (i >= points.Length)
                    i = 0;
            }
            else
            {
                // move sequentially on all point in pendulum
                // 1->2->3->4->3->2->1->2...
                if (i >= points.Length - 1 || i <= 0)
                    dir *= -1;
            }
        }
    }
}