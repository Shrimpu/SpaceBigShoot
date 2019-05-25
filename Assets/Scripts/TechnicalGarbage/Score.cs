using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    static int currentScore;
    public static int CurrentScore { get { return currentScore; } private set { currentScore = value; } }

    public static void Add(int scoreToAdd)
    {
        CurrentScore += scoreToAdd;
    }

    public static void Reset()
    {
        CurrentScore = 0;
    }

    public static void ReqisterHighscore(string name)
    {
        Highscore.Add(new Highscore.HighscoreEntry(name, CurrentScore));
    }
}