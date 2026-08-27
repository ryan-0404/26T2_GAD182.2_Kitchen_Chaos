using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreEntryUI : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField]
    private HighScoreManager highScoreManager;

    [Header("UI References")]
    [SerializeField]
    private TMP_Text finalScoreText;

    [SerializeField]
    private TMP_InputField playerNameInput;

    [Header("Scene Settings")]
    [SerializeField]
    private string mainMenuSceneName = "MainMenu";

    private bool scoreSubmitted;

    private void Start()
    {
        scoreSubmitted = false;

        DisplayFinalScore();
    }

    private void DisplayFinalScore()
    {
        if (GameManager.Instance == null)
        {
            finalScoreText.text =
                "Final Score: 0";

            return;
        }

        finalScoreText.text =
            "Final Score: " +
            GameManager.Instance.totalScore;
    }

    public void SubmitScore()
    {
        if (scoreSubmitted)
        {
            return;
        }

        if (highScoreManager == null)
        {
            Debug.LogError(
                "HighScoreManager has not been assigned.",
                this
            );

            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogError(
                "GameManager could not be found.",
                this
            );

            return;
        }

        string playerName =
            playerNameInput.text.Trim();

        if (playerName == "")
        {
            playerName = "Player";
        }

        int finalScore =
            GameManager.Instance.totalScore;

        highScoreManager.AddHighScore(
            playerName,
            finalScore
        );

        scoreSubmitted = true;

        SceneManager.LoadScene(
            mainMenuSceneName
        );
    }
}