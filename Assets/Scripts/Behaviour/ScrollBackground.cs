using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeY;

    private Vector3 startPosition;
    public GameObject child;

    public List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        //randomly pick and set one of background sprite
        int spriteId = Random.Range(0, sprites.Count);
        GetComponent<SpriteRenderer>().sprite = sprites[spriteId];
        child.GetComponent<SpriteRenderer>().sprite = sprites[spriteId];
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeY);
        transform.position = startPosition + Vector3.down * newPosition;
    }
}