using UnityEngine;
using TMPro;

public class TransitionUI : MonoBehaviour
{
    [Header("Text References")]
    public TMP_Text completedGameText;
    public TMP_Text miniGameScoreText;
    public TMP_Text totalScoreText;
    public TMP_Text livesText;
    public TMP_Text nextGameText;

    private void Start()
    {
        completedGameText.text = "Completed: " + GameManager.Instance.lastMiniGameName;
        miniGameScoreText.text = "Score Earned: " + GameManager.Instance.lastMiniGameScore;
        totalScoreText.text = "Total Score: " + GameManager.Instance.totalScore;
        livesText.text = "Lives: " + GameManager.Instance.lives;
        nextGameText.text = "Next Game: " + GameManager.Instance.GetNextMiniGameName();
    }

    public void Continue()
    {
        GameManager.Instance.LoadNextMiniGame();
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.ReturnToMainMenu();
    }
}