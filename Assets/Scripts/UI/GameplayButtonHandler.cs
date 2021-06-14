using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayButtonHandler : MonoBehaviour
{
    public Button back, exit, muteMusic, muteSound;
    public GameObject PausePanel;
    public Sprite mutedMusic, mutedSound, unmutedMusic, unmutedSound;
    private bool mutedM = false, mutedS = false;
    private float initialSoundVolume, initialMusicVolume;
    // Start is called before the first frame update
    void Start()
    {
        back.onClick.AddListener(() => UnpauseGame());
        exit.onClick.AddListener(() => BackToMenu());
        muteMusic.onClick.AddListener(() => MusicToggle());
        muteSound.onClick.AddListener(() => SoundsToggle());

        initialSoundVolume = MusicManager.Instance.GetSoundsVolume();
        initialMusicVolume = MusicManager.Instance.GetMusicVolume();

        if (MusicManager.Instance.GetSoundsVolume() == 0f)
        {
            mutedS = true;
            muteSound.GetComponent<Image>().sprite = mutedSound;
        }
        if(MusicManager.Instance.GetMusicVolume() == 0f)
        {
            mutedM = true;
            muteMusic.GetComponent<Image>().sprite = mutedMusic;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Cancel") && LevelManager.PlayerAlive)
        {
            if (Cursor.visible)
                Cursor.visible = false;
            else
                Cursor.visible = true;
            if (!PausePanel.activeSelf)
            {
                Cursor.visible = true;
                Time.timeScale = 0;
                PausePanel.SetActive(true);
            }
            else
            {
                UnpauseGame();
            }
        }
    }
    void UnpauseGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }
    void BackToMenu()
    {
        MusicManager.Instance.PlayMusic(MusicManager.Music.Menu);
        MusicManager.Instance.SetSoundsVolume(initialSoundVolume);
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }
    void MusicToggle()
    {
        if (initialMusicVolume > 0f)
        {
            if (!mutedM)
            {
                MusicManager.Instance.SetMusicVolume(0f);
                muteMusic.GetComponent<Image>().sprite = mutedMusic;
            }
            else
            {
                MusicManager.Instance.SetMusicVolume(initialMusicVolume);
                muteMusic.GetComponent<Image>().sprite = unmutedMusic;
            }
            mutedM = !mutedM;
        }
    }
    void SoundsToggle()
    {
        if (initialSoundVolume > 0f)
        {
            if (!mutedS)
            {
                MusicManager.Instance.SetSoundsVolume(0f);
                muteSound.GetComponent<Image>().sprite = mutedSound;
            }
            else
            {
                MusicManager.Instance.SetSoundsVolume(initialSoundVolume);
                muteSound.GetComponent<Image>().sprite = unmutedSound;
            }
            mutedS = !mutedS;
        }
    }
}
