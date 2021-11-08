/* A standard example of saving and loading Data using JSON file.
 * Dataflow: AddNew -> SaveData -> SaveDataFormat -> JSON -> SaveDataFormat -> GetSaveData
 * Use SaveDataFormat to increase Savedata protection level.
 * And use SaveDataFormat can simplify the structure, save part of the data.
 */
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataManager
{
    [SerializeField] private static int RankNum = 10;

    public class Score
    {
        // Must be public to be write to JSON.
        public string userName { get; private set; }
        public int score { get; private set; }

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


    // Interact with JSON file.
    // Cannot define ScoreFormat in SaveDataFormat.
    [System.Serializable]
    public class ScoreFormat
    {
        public string userName;
        public int score;
        public ScoreFormat(string newUserName, int newScore)
        {
            userName = newUserName;
            score = newScore;
        }
    }
    [System.Serializable]
    private class SaveDataFormat
    {
        public List<ScoreFormat> scoresFormat= new List<ScoreFormat>();
    }
    public static void SaveToJSON()  // Save data between sessions (to JSON)
    {
        SaveDataFormat saveDataFormat = new SaveDataFormat();
        foreach(Score score in _saveData.scores)
        {
            saveDataFormat.scoresFormat.Add(new ScoreFormat(score.userName, score.score));
        }
        string json = JsonUtility.ToJson(saveDataFormat);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public static void LoadFromJSON()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataFormat saveDataFormat = new SaveDataFormat();
            saveDataFormat = JsonUtility.FromJson<SaveDataFormat>(json);
            foreach(ScoreFormat scoreFormat in saveDataFormat.scoresFormat)
            {
                _saveData.scores.Add(new Score(scoreFormat.userName, scoreFormat.score));
            }
        }
    }
}
