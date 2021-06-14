using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreRecord
{
    public string PlayerName;
    public uint Score;

    public ScoreRecord(string name, uint score)
    {
        PlayerName = name;
        Score = score;
    }
    public ScoreRecord(string Json)
    {
        ScoreRecord sr = JsonUtility.FromJson<ScoreRecord>(Json);
        PlayerName = sr.PlayerName;
        Score = sr.Score;
    }
    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}
