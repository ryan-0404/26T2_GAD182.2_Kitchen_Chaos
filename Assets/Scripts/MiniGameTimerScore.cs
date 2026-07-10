using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameTimerScore : MonoBehaviour
{
    [Header("Mini Game Settings")]
    [SerializeField] private string miniGameName = "Mini Game";
    [SerializeField] private float timeLimit = 10f;
    [SerializeField] private float gameNameDisplayTime = 1f;

    [Header("UI References")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text gameNameText;
    [SerializeField] private Button pauseButton;

    private float timeRemaining;
    private float elapsedTime;

    private bool timerStarted;
    private bool gameEnded;

    private void Awake()
    {
        HidePauseButton();
    }

    private void Start()
    {
        timeRemaining = timeLimit;
        elapsedTime = 0f;
        timerStarted = false;
        gameEnded = false;

        if (timerText != null)
        {
            timerText.text = string.Empty;
        }
        else
        {
            Debug.LogWarning(
                "Timer Text has not been assigned in MiniGameTimerScore.",
                this
            );
        }

        if (gameNameText != null)
        {
            gameNameText.text = miniGameName;
            gameNameText.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning(
                "Game Name Text has not been assigned in MiniGameTimerScore.",
                this
            );
        }

        HidePauseButton();

        StartCoroutine(StartGameAfterDelay());
    }

    private IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(gameNameDisplayTime);

        if (gameNameText != null)
        {
            gameNameText.gameObject.SetActive(false);
        }

        timerStarted = true;

        ShowPauseButton();
        UpdateTimerUI();
    }

    private void Update()
    {
        if (!timerStarted || gameEnded)
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            UpdateTimerUI();
            EndMiniGame(false);
            return;
        }

        UpdateTimerUI();
    }

    public void CompleteTask()
    {
        if (!timerStarted || gameEnded)
        {
            return;
        }

        EndMiniGame(true);
    }

    public void FailTask()
    {
        if (!timerStarted || gameEnded)
        {
            return;
        }

        EndMiniGame(false);
    }

    private void EndMiniGame(bool succeeded)
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;
        timerStarted = false;

        HidePauseButton();

        int scoreEarned = succeeded ? CalculateScore() : 0;

        if (GameManager.Instance == null)
        {
            Debug.LogError(
                "MiniGameTimerScore could not find GameManager. " +
                "Start the game from the MainMenu scene and ensure " +
                "GameManager.cs is attached to a persistent GameObject.",
                this
            );

            return;
        }

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
        if (timerText == null)
        {
            return;
        }

        int displayedTime = Mathf.CeilToInt(timeRemaining);
        timerText.text = "Time: " + displayedTime;
    }

    private void HidePauseButton()
    {
        if (pauseButton == null)
        {
            return;
        }

        pauseButton.interactable = false;
        pauseButton.gameObject.SetActive(false);
    }

    private void ShowPauseButton()
    {
        if (pauseButton == null)
        {
            Debug.LogError(
                "Pause Button has not been assigned in MiniGameTimerScore.",
                this
            );

            return;
        }

        pauseButton.gameObject.SetActive(true);
        pauseButton.interactable = true;
    }
}