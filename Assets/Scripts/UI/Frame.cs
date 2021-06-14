using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Frame : MonoBehaviour
{
    public Image frameImage;
    public GameObject countFrame;

    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = countFrame.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void SetCount(int count)
    {
        if (count <= 0)
        {
            frameImage.color = Color.clear;
            countFrame.SetActive(false);
        }
        else
        {
            countFrame.SetActive(true);
            frameImage.color = Color.white;
            text.text = count.ToString();
        }
    }
    public void SetImage(Sprite image)
    {
        frameImage.color = Color.white;
        frameImage.sprite = image;
    }
}
