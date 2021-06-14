using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject[] bar;
    public bool isVertical;
    private RectTransform BarRT;
    // Start is called before the first frame update
    void Start()
    {
        BarRT = GetComponent<RectTransform>();
    }
    public void SetHealthBar(float percent)
    {
        if(percent >= 0)
        {
            if(isVertical)
                BarRT.localScale = new Vector3(BarRT.localScale.x, percent, BarRT.localScale.z);
            else
                BarRT.localScale = new Vector3(percent, BarRT.localScale.y, BarRT.localScale.z);

            if (bar.Length == 3)
            {
                if (percent > 0.7f)
                {
                    bar[0].SetActive(true);
                    bar[1].SetActive(false);
                    bar[2].SetActive(false);
                }
                if(percent <= 0.7f && percent >= 0.3f)
                {
                    bar[0].SetActive(false);
                    bar[1].SetActive(true);
                    bar[2].SetActive(false);
                }
                if(percent < 0.3f)
                {
                    bar[0].SetActive(false);
                    bar[1].SetActive(false);
                    bar[2].SetActive(true);
                }
            }
        }
        else
        {
            if(isVertical)
                BarRT.localScale = new Vector3(BarRT.localScale.x, 0f, BarRT.localScale.z);
            else
                BarRT.localScale = new Vector3(0f, BarRT.localScale.y, BarRT.localScale.z);
        }
    }
}
