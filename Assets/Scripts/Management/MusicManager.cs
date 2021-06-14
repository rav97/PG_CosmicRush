using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance = null;
    public static MusicManager Instance
    {
        get { return instance; }
    }
    public AudioClip[] clips;
    public enum Music : int { Menu = 0, Gameplay=1, BossBattle = 2};
    private AudioSource audioSource;
    private float MusicVolume = 1f, SoundsVolume = 1f;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayMusic(Music music)
    {
        audioSource.Stop();
        audioSource.clip = clips[(int)music];
        audioSource.Play();
    }
    public void PlayMusic()
    {
        audioSource.Play();
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }
    public void PauseMusic()
    {
        audioSource.Pause();
    }
    public void UnpauseMusic()
    {
        audioSource.UnPause();
    }
    public void SetMusicVolume(float value)
    {
        MusicVolume = value;
        audioSource.volume = value;
    }
    public void SetSoundsVolume(float value)
    {
        SoundsVolume = value;
    }
    public float GetMusicVolume()
    {
        return MusicVolume;
    }
    public float GetSoundsVolume()
    {
        return SoundsVolume;
    }
}
