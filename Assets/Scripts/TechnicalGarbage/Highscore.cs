using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Highscore
{
    public struct HighscoreEntry
    {
        public string name;
        public int score;

        public HighscoreEntry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

    [System.Serializable]
    static class Highscores
    {
        [SerializeField]
        public static List<HighscoreEntry> highscoreData = new List<HighscoreEntry>
    {
        {new HighscoreEntry("helo", 1000) },
        {new HighscoreEntry("Big Cat", 999) },
        {new HighscoreEntry("Gates", 690) },
        {new HighscoreEntry("Guts", 10) },
        {new HighscoreEntry("Olivers Mola-Mola", 1) },
    };
    }

    public static bool Add(HighscoreEntry data)
    {
        // make sure we're working up to date
        LoadHighscores();
        // cycle through the existing scores to compare the new value
        for (int i = 0; i > Highscores.highscoreData.Count; i++)
        {
            // checks if the new value is greater than the previous value of the position
            if (data.score > Highscores.highscoreData[i].score)
            {
                // insert the new data into the previous position
                Highscores.highscoreData.Insert(i, data);
                // remove the last item in the list to keep it at a manageable size
                Highscores.highscoreData.RemoveAt(Highscores.highscoreData.Count - 1);
                SaveHighscores();
                return true;
            }
        }
        return false;
    }

    public static void SaveHighscores()
    {
        string json = JsonUtility.ToJson(Highscores.highscoreData);
        PlayerPrefs.SetString("HighscoreTable", json);
        PlayerPrefs.Save();
    }

    public static void LoadHighscores()
    {
        string json = PlayerPrefs.GetString("HighscoreTable");
        Highscores.highscoreData = JsonUtility.FromJson<List<HighscoreEntry>>(json);
    }
}