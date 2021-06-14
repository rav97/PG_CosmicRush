using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDescription : MonoBehaviour
{
    public GameObject classic, speed, defense, ofense;
    public Button leftBtn, rightBtn;
    // Start is called before the first frame update
    void Start()
    {
        classic.SetActive(true);
        speed.SetActive(false);
        defense.SetActive(false);
        ofense.SetActive(false);
    }
    public void SetShipType(float value)
    {
        //float value = sb.value;
        if (value <= 0.16f)
        {
            leftBtn.interactable = false;
            GameManager.shipType = 0;
            classic.SetActive(true);
            speed.SetActive(false);
            defense.SetActive(false);
            ofense.SetActive(false);
        }
        if (value > 0.16f && value < 0.50f)
        {
            leftBtn.interactable = true;
            GameManager.shipType = 1;
            classic.SetActive(false);
            speed.SetActive(true);
            defense.SetActive(false);
            ofense.SetActive(false);
        }
        if (value >= 0.50f && value < 0.84f)
        {
            rightBtn.interactable = true;
            GameManager.shipType = 2;
            classic.SetActive(false);
            speed.SetActive(false);
            defense.SetActive(true);
            ofense.SetActive(false);
        }
        if (value >= 0.85f)
        {
            rightBtn.interactable = false;
            GameManager.shipType = 3;
            classic.SetActive(false);
            speed.SetActive(false);
            defense.SetActive(false);
            ofense.SetActive(true);
        }

        //Debug.Log("Ship type = " + GameManager.shipType);
    }
}
