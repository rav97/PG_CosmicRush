using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class GameRank
{
    public List<ScoreRecord> Rank;

    public GameRank()
    {
        Rank = new List<ScoreRecord>();
    }
    public GameRank(string filepath)
    {
        if (File.Exists(filepath))
        {
            try
            {
                string json = Cryptography.ReadAndDecrypt(filepath);
                GameRank gr = JsonUtility.FromJson<GameRank>(json);
                Rank = gr.Rank;
            }
            catch(Exception e)
            {
                // any error means that file is corrupted, so rank is deleted
                Debug.LogWarning(e);
                Rank = new List<ScoreRecord>();
            }
        }
        else
        {
            Rank = new List<ScoreRecord>();
        }
    }
    public void AddToRank(string name, uint score)
    {
        Rank.Add(new ScoreRecord(name, score));
        Rank = Rank.OrderByDescending(x => x.Score).Take(10).ToList();
    }
    public int ReturnPosition(string name, uint score)
    {
        return Rank.FindIndex(x => x.Score == score && x.PlayerName == name);
    }
    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}
