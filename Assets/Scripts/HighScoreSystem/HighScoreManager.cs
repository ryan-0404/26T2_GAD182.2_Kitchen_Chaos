using System;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    private const string HighScoreSaveKey = "HighScores";

    [SerializeField] private int maximumScores = 10;

    private List<HighScoreEntry> highScores =
        new List<HighScoreEntry>();

    private void Awake()
    {
        LoadHighScores();
    }

    public void AddHighScore(string playerName, int score)
    {
        HighScoreEntry newEntry =
            new HighScoreEntry(playerName, score);

        highScores.Add(newEntry);

        SortHighScores();

        if (highScores.Count > maximumScores)
        {
            highScores.RemoveRange(
                maximumScores,
                highScores.Count - maximumScores
            );
        }

        SaveHighScores();
    }

    public List<HighScoreEntry> GetHighScores()
    {
        return highScores;
    }

    private void SortHighScores()
    {
        highScores.Sort(CompareScores);
    }

    private int CompareScores(
        HighScoreEntry firstEntry,
        HighScoreEntry secondEntry
    )
    {
        return secondEntry.score.CompareTo(
            firstEntry.score
        );
    }

    private void SaveHighScores()
    {
        HighScoreSaveData saveData =
            new HighScoreSaveData();

        saveData.entries = highScores;

        string json =
            JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString(
            HighScoreSaveKey,
            json
        );

        PlayerPrefs.Save();
    }

    private void LoadHighScores()
    {
        if (!PlayerPrefs.HasKey(HighScoreSaveKey))
        {
            highScores =
                new List<HighScoreEntry>();

            return;
        }

        string json =
            PlayerPrefs.GetString(
                HighScoreSaveKey
            );

        HighScoreSaveData saveData =
            JsonUtility.FromJson<HighScoreSaveData>(
                json
            );

        if (saveData == null ||
            saveData.entries == null)
        {
            highScores =
                new List<HighScoreEntry>();

            return;
        }

        highScores = saveData.entries;

        SortHighScores();
    }
}

[Serializable]
public class HighScoreSaveData
{
    public List<HighScoreEntry> entries =
        new List<HighScoreEntry>();
}