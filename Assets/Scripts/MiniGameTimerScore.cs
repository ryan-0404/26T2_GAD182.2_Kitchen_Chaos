using UnityEngine;
using TMPro;
using System.Collections;

public class MiniGameTimerScore : MonoBehaviour
{
    [Header("Mini Game Settings")]
    [SerializeField] private string miniGameName = "Mini Game";
    [SerializeField] private float timeLimit = 10f;
    [SerializeField] private float gameNameDisplayTime = 1f;

    [Header("UI References")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text gameNameText;

    private float timeRemaining;
    private float elapsedTime;
    private bool timerStarted;
    private bool gameEnded;

    private void Start()
    {
        timeRemaining = timeLimit;
        elapsedTime = 0f;
        timerStarted = false;
        gameEnded = false;

        if (timerText != null)
        {
            timerText.text = "";
        }

        if (gameNameText != null)
        {
            gameNameText.text = miniGameName;
            gameNameText.gameObject.SetActive(true);
        }

        StartCoroutine(StartGameAfterDelay());
    }

    private IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSeconds(gameNameDisplayTime);

        if (gameNameText != null)
        {
            gameNameText.gameObject.SetActive(false);
        }

        timerStarted = true;
        UpdateTimerUI();
    }

    private void Update()
    {
        if (gameEnded || !timerStarted) return;

        elapsedTime += Time.deltaTime;
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            UpdateTimerUI();
            EndMiniGame(false);
            return;
        }

        UpdateTimerUI();
    }

    public void CompleteTask()
    {
        if (gameEnded || !timerStarted) return;

        EndMiniGame(true);
    }

    public void FailTask()
    {
        if (gameEnded || !timerStarted) return;

        EndMiniGame(false);
    }

    private void EndMiniGame(bool succeeded)
    {
        gameEnded = true;

        int scoreEarned = succeeded ? CalculateScore() : 0;

        GameManager.Instance.CompleteMiniGame(scoreEarned, succeeded);
    }

    private int CalculateScore()
    {
        int secondCompleted = Mathf.CeilToInt(elapsedTime);
        secondCompleted = Mathf.Clamp(secondCompleted, 1, 10);

        return 1100 - secondCompleted * 100;
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining);
        }
    }
}