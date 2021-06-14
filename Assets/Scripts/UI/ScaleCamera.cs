using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ScaleCamera : MonoBehaviour
{
    // Set this to the in-world distance between the left & right edges of your scene.
    public float sceneWidth = 18;
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();

        float unitsPerPixel = sceneWidth / Screen.width;

        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        if (desiredHalfHeight > 5.0f)
            _camera.orthographicSize = desiredHalfHeight;
        else
            _camera.orthographicSize = 5.0f;
    }
}
