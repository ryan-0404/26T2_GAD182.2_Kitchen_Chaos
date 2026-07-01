using UnityEngine;
using TMPro;

public class ResultsUI : MonoBehaviour
{
    [Header("Text References")]
    public TMP_Text finalScoreText;
    public TMP_Text gamesCompletedText;
    public TMP_Text resultMessageText;

    private void Start()
    {
        finalScoreText.text = "Final Score: " + GameManager.Instance.totalScore;

        gamesCompletedText.text = "Mini Games Completed: " 
            + GameManager.Instance.currentMiniGameIndex 
            + " / " 
            + GameManager.Instance.miniGameScenes.Length;

        if (GameManager.Instance.lives <= 0)
        {
            resultMessageText.text = "Game Over!";
        }
        else
        {
            resultMessageText.text = "Food Frenzy Complete!";
        }
    }

    public void PlayAgain()
    {
        GameManager.Instance.StartNewRun();
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.ReturnToMainMenu();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}