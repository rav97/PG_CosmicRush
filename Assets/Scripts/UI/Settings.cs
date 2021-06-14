using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider MusicSlider, SoundSlider;
    // Start is called before the first frame update
    void Start()
    {
        MusicSlider.value = MusicManager.Instance.GetMusicVolume();
        SoundSlider.value = MusicManager.Instance.GetSoundsVolume();
    }
    public void SetMusic(Slider slider)
    {
        Debug.Log("Music volume set to " + slider.value);
        MusicManager.Instance.SetMusicVolume(slider.value);
    }
    public void SetSounds(Slider slider)
    {
        Debug.Log("Sounds volume set to " + slider.value);
        MusicManager.Instance.SetSoundsVolume(slider.value);
    }
}
