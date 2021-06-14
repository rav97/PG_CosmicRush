using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(CanvasScaler))]
public class DynamicCanvas : MonoBehaviour
{
    private CanvasScaler canvasScaler;
    // Start is called before the first frame update
    void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();

        if (Camera.main.aspect <= 1.8f)
        {
            //Debug.Log("Aspect ration lower than 16:9");
            canvasScaler.matchWidthOrHeight = 0;
        }
        else
        {
            //Debug.Log("Aspect ration higher than 16:9");
            canvasScaler.matchWidthOrHeight = 1;
        }
    }
}
