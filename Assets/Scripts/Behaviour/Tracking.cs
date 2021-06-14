using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
    public float MaxAngle;
    private Transform target;
    private float MaxAngleTMP;
    // Start is called before the first frame update
    void Start()
    {
        // the limit of rotation
        MaxAngleTMP = 180.0f - MaxAngle;
        try
        {
            // by default the player is the target
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch (System.NullReferenceException ex)
        {
            Debug.LogWarning(ex);
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // calculate the angle of rotation so that gameobject looks towards the target
            Vector3 dif = target.position - transform.position;
            float rotationZ = -Mathf.Atan2(dif.x, dif.y) * Mathf.Rad2Deg;

            //limitation of rotation angle
            if (rotationZ < MaxAngleTMP && rotationZ > 0f)
                rotationZ = MaxAngleTMP;
            if (rotationZ > -MaxAngleTMP && rotationZ < 0f)
                rotationZ = -MaxAngleTMP;

            // applying rotation only if target is below attached gameobject
            if (dif.y < 0)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, rotationZ), Time.deltaTime * 5);
                //transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
        else
        {
            // if target is destroyed, looks at the last faced direction
            transform.rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z);
        }
    }
}
