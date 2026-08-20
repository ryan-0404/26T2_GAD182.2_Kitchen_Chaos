using TMPro;
using UnityEngine;

public class ResultsUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text finalScoreText;

    private void Start()
    {
        DisplayResults();
    }

    private void DisplayResults()
    {
        if (finalScoreText == null)
        {
            Debug.LogError(
                "ResultsUI requires a Final Score Text reference.",
                this
            );

            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogError(
                "ResultsUI could not find GameManager.Instance.",
                this
            );

            finalScoreText.text = "Final Score: 0";
            return;
        }

        finalScoreText.text =
            "Final Score: " +
            GameManager.Instance.totalScore;
    }

    public void PlayAgain()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError(
                "ResultsUI could not find GameManager.Instance.",
                this
            );

            return;
        }

        GameManager.Instance.StartNewRun();
    }

    public void ReturnToMainMenu()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError(
                "ResultsUI could not find GameManager.Instance.",
                this
            );

            return;
        }

        GameManager.Instance.ReturnToMainMenu();
    }
}