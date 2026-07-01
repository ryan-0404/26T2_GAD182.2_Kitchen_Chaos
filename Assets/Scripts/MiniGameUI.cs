using UnityEngine;
using TMPro;

public class MiniGameUI : MonoBehaviour
{
    [Header("Text References")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text livesText;
    public TMP_Text gameNameText;

    [Header("Mini Game Settings")]
    public string miniGameName;
    public float gameTime = 30f;

    private int miniGameScore;
    private float timer;
    private bool gameEnded;

    private void Start()
    {
        timer = gameTime;
        miniGameScore = 0;
        gameEnded = false;

        gameNameText.text = miniGameName;
        UpdateUI();
    }

    private void Update()
    {
        if (gameEnded) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            EndMiniGame(true);
        }

        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (gameEnded) return;

        miniGameScore += amount;
        UpdateUI();
    }

    public void LoseMiniGame()
    {
        EndMiniGame(false);
    }

    public void WinMiniGame()
    {
        EndMiniGame(true);
    }

    private void EndMiniGame(bool succeeded)
    {
        if (gameEnded) return;

        gameEnded = true;
        GameManager.Instance.CompleteMiniGame(miniGameScore, succeeded);
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + miniGameScore;
        timerText.text = "Time: " + Mathf.CeilToInt(timer);
        livesText.text = "Lives: " + GameManager.Instance.lives;
    }
}