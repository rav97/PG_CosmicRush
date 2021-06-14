using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour
{
    private AudioSource aS;
    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
        aS.volume = MusicManager.Instance.GetSoundsVolume();
        aS.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        aS.volume = MusicManager.Instance.GetSoundsVolume();

        if (Time.timeScale < 1)
            aS.Pause();
        else
            aS.UnPause();
    }
}
