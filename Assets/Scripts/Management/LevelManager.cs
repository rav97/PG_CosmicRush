using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Players;

    public WavesDifficulties[] WavesByDifficulty;
    private Queue<KeyValuePair<int, int>> lastWaves;

    public GameObject[] bonuses;
    private float nextBonus;
    
    public static uint Score;

    public static bool WaveExist;
    public static bool PlayerAlive;

    public GameObject[] bossWaves;
    public GameObject BossPanel;
    public float bossThereshold;
    private float nextBoss;
    private int BossID;

    public GameObject gameoverScreen;

    // Awake is called at initialize component before Start()
    void Awake()
    {
        lastWaves = new Queue<KeyValuePair<int, int>>();
        WaveExist = false;
        Score = 0;
        nextBoss = bossThereshold;
        BossID = 0;
        gameoverScreen.SetActive(false);
        PlayerAlive = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        MusicManager.Instance.PlayMusic(MusicManager.Music.Gameplay);
        Time.timeScale = 1f;
        nextBonus = Time.time + Random.Range(10f, 30f);
        Instantiate(Players[GameManager.shipType], new Vector3(0,-2.5f), Quaternion.Euler(0f,0f,0f));
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerAlive)
        {
            if (!WaveExist)
            {
                StartCoroutine(CreateNextWave());
            }
            if (Time.time > nextBonus)
            {
                SpawnBonus();
            }
        }
        else
        {
            if(!gameoverScreen.activeSelf)
                StartCoroutine(EndTheGame());
        }
    }
    IEnumerator CreateNextWave()
    {
        WaveExist = true;

        //check if Boss should be spawn
        if (Score < nextBoss)
        {
            int dif = GetWaveDifficulty();
            int waveId;

            //Selects one of existing waves and check if it not occurred lately
            do
            {
                waveId = Random.Range(0, WavesByDifficulty[dif].Wave.Length);
            } while (CheckRepeat(dif, waveId, 10));

            //Create new wave after 2.5 seconds
            yield return new WaitForSeconds(2.5f);

            Instantiate(WavesByDifficulty[dif].Wave[waveId], new Vector3(-15f, 6f), transform.rotation);
        }
        else
        {
            //Create new wave after 2.5 seconds
            yield return new WaitForSeconds(2.5f);

            SpawnBoss();
        }
    }
    private void SpawnBoss()
    {
        BossPanel.SetActive(true);
        MusicManager.Instance.PlayMusic(MusicManager.Music.BossBattle);
        Instantiate(bossWaves[BossID % bossWaves.Length], new Vector3(-15f, 6f), transform.rotation);
        nextBoss += bossThereshold;
        BossID++;
    }
    private void SpawnBonus()
    {
        int bonusId = Random.Range(0, bonuses.Length);

        Instantiate(bonuses[bonusId], new Vector3(Random.Range(-7f, 7f), 5.5f), Quaternion.Euler(0, 0, 180));
        nextBonus = Time.time + Random.Range(20f, 45f);
    }
    private IEnumerator EndTheGame()
    {
        gameoverScreen.SetActive(true);

        yield return new WaitForSeconds(1);

        GameManager.gameRank.AddToRank(GameManager.playerName, Score);
        Cryptography.EncryptAndSave(GameManager.gameRank.ToString(), Application.persistentDataPath + "/" + "GameRank.gr");
        GameManager.shipType = 0;

        yield return new WaitForSeconds(2);

        Destroy(gameObject);
        Cursor.visible = true;
        MusicManager.Instance.PlayMusic(MusicManager.Music.Menu);
        SceneManager.LoadScene("Scores");
    }
    private int GetWaveDifficulty()
    {
        int number = Random.Range(0, 1000);
        if(Score < 25000)
        {
            if(Score < 15000)
            {
                //High chance to easy wave low chance to medium wave
                if (number < 900)
                    return 0;
                else
                    return 1;
            }
            // High chance to medium wave, medium chance to easy wave and low chance to hard wave
            if(number < 950)
            {
                if (number < 200)
                    return 0;
                return 1;
            }
            return 2;
        }
        // High chance to hard wave and low to medium wave
        if (number < 850)
            return 2;
        else
            return 1;
    }
    private bool CheckRepeat(int dif, int waveId, int howMany)
    {
        bool repeat = false;

        if (lastWaves.Count == 0)
            lastWaves.Enqueue(new KeyValuePair<int, int>(dif, waveId));
        else
        {
            foreach (KeyValuePair<int, int> kvp in lastWaves)
            {
                if (kvp.Key == dif && kvp.Value == waveId)
                {
                    repeat = true;
                    break;
                }
            }
            if (!repeat)
            {
                if (lastWaves.Count >= howMany)
                    lastWaves.Dequeue();

                lastWaves.Enqueue(new KeyValuePair<int, int>(dif, waveId));
            }
        }
        return repeat;
    }
}
