using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectPrefab;
    public float spawnSpeed;
    public int quantity;
    public float xMin, xMax;

    private float nextSpawn;
    private bool ended = false;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn && quantity > 0)
        {
            //select random x position
            float x = Random.Range(xMin, xMax);
            //select random prefab
            int i = Random.Range(0, objectPrefab.Length);
            // instantiate selected gameobject on  y = 6 and selected X coordinates
            Instantiate(objectPrefab[i], new Vector3(x, 6.0f), objectPrefab[i].transform.rotation);
            quantity--;
            nextSpawn = Time.time + spawnSpeed;
        }
        else if (quantity <= 0)
        {
            // destroy if created certain amount of gameobject after 3 seconds
            if(!ended)
                StartCoroutine(ReleaseWave());
        }
    }
    private IEnumerator ReleaseWave()
    {
        ended = true;

        yield return new WaitForSeconds(3.0f);

        LevelManager.WaveExist = false;
        Destroy(gameObject);
    }
}
