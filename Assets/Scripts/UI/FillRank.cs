using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FillRank : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().SetText(GetRankTexted());
    }
    public string GetRankTexted()
    {
        string TEXT = "";
        string SEP = ".";
        int SIZE = 55;

        if (GameManager.gameRank.Rank.Count > 0)
        {
            foreach (ScoreRecord score in GameManager.gameRank.Rank)
            {
                int fill = SIZE - score.PlayerName.Length - score.Score.ToString().Length;
                TEXT += score.PlayerName;
                for (int i = fill; i >= 0; i--)
                {
                    TEXT += SEP;
                }
                TEXT += score.Score.ToString() + "\n";
            }
            return TEXT;
        }
        else
        {
            return "There is no rank to display. Play the game to fill it.";
        }
    }
}
