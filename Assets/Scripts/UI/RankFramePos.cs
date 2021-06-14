using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RankFramePos : MonoBehaviour
{
    private RectTransform rectTransform;
    private Image image;
    private float min = 0f, max = 1f, t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();

        image.enabled = false;
        SetPosition();
    }
    // create pulsing effect
    void Update()
    {
        image.color = new Color(1, 1, 1, Mathf.Lerp(min, max, t));

        t += Time.deltaTime;

        if(t >= 1.0f)
        {
            float tmp = max;
            max = min;
            min = tmp;
            t = 0f;
        }
    }
    void SetPosition()
    {
        // returns rank position of last gameplay
        int rankPosition = GameManager.gameRank.ReturnPosition(GameManager.playerName, LevelManager.Score);
        float posy = 176.0f;
        for(int i = 0; i <= 9; i++)
        {
            if(i == rankPosition)
            {
                rectTransform.anchoredPosition = new Vector3(0, posy, 0);
                image.enabled = true;
                break;
            }
            posy -= 50.0f;
        }
        LevelManager.Score = 0;
    }
}
