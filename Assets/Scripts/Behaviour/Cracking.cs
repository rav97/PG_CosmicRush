using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cracking : MonoBehaviour
{
    public SpriteRenderer crackedSprite;

    //set alpha channel to certain value
    public void SetAlpha(float alpha)
    {
        //Debug.Log("Alpha = " + alpha);
        if (alpha > 1f)
            alpha = 1f;
        if (alpha < 0f)
            alpha = 0f;

        crackedSprite.color = new Color(1f, 1f, 1f, alpha);
    }
}
