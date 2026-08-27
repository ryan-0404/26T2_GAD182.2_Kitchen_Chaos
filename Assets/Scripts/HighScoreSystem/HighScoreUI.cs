using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreUI : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField]
    private HighScoreManager highScoreManager;

    [Header("UI References")]
    [SerializeField]
    private TMP_Text highScoreText;

    [Header("Scene Settings")]
    [SerializeField]
    private string mainMenuSceneName = "MainMenu";

    private void Start()
    {
        DisplayHighScores();
    }

    private void DisplayHighScores()
    {
        if (highScoreManager == null)
        {
            Debug.LogError(
                "HighScoreManager has not been assigned.",
                this
            );

            return;
        }

        if (highScoreText == null)
        {
            Debug.LogError(
                "High Score Text has not been assigned.",
                this
            );

            return;
        }

        List<HighScoreEntry> scores =
            highScoreManager.GetHighScores();

        if (scores.Count == 0)
        {
            highScoreText.text =
                "No scores yet.";

            return;
        }

        string displayText = "";

        for (int i = 0; i < scores.Count; i++)
        {
            displayText +=
                (i + 1) +
                ". " +
                scores[i].playerName +
                "     " +
                scores[i].score;

            if (i < scores.Count - 1)
            {
                displayText += "\n";
            }
        }

        highScoreText.text = displayText;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(
            mainMenuSceneName
        );
    }
}