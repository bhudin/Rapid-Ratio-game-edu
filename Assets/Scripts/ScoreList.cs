using System;
using UnityEngine;

public class ScoreList : IComparable<ScoreList>
{
    public string nameString, level;
    public int scoreInt;
    public ScoreList(string newName, int newScore, string newLevel)
    {
        nameString = newName;
        scoreInt = newScore;
        level = newLevel;
        PlayerPrefs.SetInt("skor", newScore);
    }
    public int CompareTo(ScoreList other)
    {
        if(other == null)
        {
            return 1;
        }
        return scoreInt - other.scoreInt;
    }
}
