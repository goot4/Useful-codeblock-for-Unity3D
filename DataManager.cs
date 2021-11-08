/* A standard example of saving and loading Data using JSON file.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataManager
{
    [SerializeField] private static int RankNum = 10;

    [System.Serializable]
    public class Score
    {
        // Must be public to be write to JSON.
        public string userName;
        public int score;

        public Score(string newUserName, int newScore)
        {
            userName = newUserName;
            score = newScore;
        }
        public bool IsGreater(int comparedScore)
        {
            return score >= comparedScore;
        }
    }
    [System.Serializable]
    private class SaveData
    {
        public List<Score> scores = new List<Score>();
    }

    private static SaveData _saveData = new SaveData();
    public static void AddNewScore(string userName, int score)  // Add a new score to _savedata when Game Over, also keep Count=RankNum
    {
        Score newScore = new Score(userName, score);

        // Insert new score
        if (_saveData.scores.Count == 0 || !_saveData.scores[0].IsGreater(score)) { _saveData.scores.Insert(0, newScore); }
        else
        {
            for (int i = _saveData.scores.Count - 1; i >= 0; i--)
            {
                if (_saveData.scores[i].IsGreater(score))
                {
                    _saveData.scores.Insert(i + 1, newScore);
                    break;
                }
            }
        }

        // Delete if Count>RankNum.
        if (_saveData.scores.Count > RankNum)
        {
            _saveData.scores.RemoveAt(_saveData.scores.Count - 1);
        }

    }

    public static List<Score> GetSaveDate()
    {
        List<Score> scores = _saveData.scores;
        return scores;
    }
    public static void SaveToJSON()  // Save data between sessions (to JSON)
    {
        string json = JsonUtility.ToJson(_saveData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public static void LoadFromJSON()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            _saveData = JsonUtility.FromJson<SaveData>(json);
        }
    }
}
