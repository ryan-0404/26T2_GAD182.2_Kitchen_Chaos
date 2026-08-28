using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameTimerScore : MonoBehaviour
{
    [Header("Mini Game Settings")]
    [SerializeField] private string miniGameName = "Mini Game";
    [SerializeField] private string instruction = "";
    [SerializeField] private float timeLimit = 10f;
    [SerializeField] private float gameNameDisplayTime = 1f;

    [Header("UI References")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text gameNameText;
    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private Button pauseButton;

    private float timeRemaining;
    private float elapsedTime;

    private bool timerStarted;
    private bool gameEnded;

    public bool GameplayStarted
    {
        get
        {
            return timerStarted && !gameEnded;
        }
    }

    public bool GameEnded
    {
        get
        {
            return gameEnded;
        }
    }

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
            timerText.text = "";
        }

        if (gameNameText != null)
        {
            gameNameText.text = miniGameName;
            gameNameText.gameObject.SetActive(true);
        }

        if (instructionText != null)
        {
            instructionText.text = instruction;
            instructionText.gameObject.SetActive(true);
        }

        HidePauseButton();

        StartCoroutine(StartGameAfterDelay());
    }

    private IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSeconds(gameNameDisplayTime);

        if (gameEnded)
        {
            yield break;
        }

        if (gameNameText != null)
        {
            gameNameText.gameObject.SetActive(false);
        }

        if (instructionText != null)
        {
            instructionText.gameObject.SetActive(false);
        }

        timerStarted = true;

        UpdateTimerUI();
        ShowPauseButton();
    }

    private void Update()
    {
        if (gameEnded)
        {
            return;
        }

        if (!timerStarted)
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
        if (gameEnded || !timerStarted)
        {
            return;
        }

        EndMiniGame(true);
    }

    public void FailTask()
    {
        if (gameEnded || !timerStarted)
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

        int scoreEarned = 0;

        if (succeeded)
        {
            scoreEarned = CalculateScore();
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.CompleteMiniGame(
                scoreEarned,
                succeeded
            );
        }
        else
        {
            Debug.LogError(
                "MiniGameTimerScore could not find GameManager.Instance.",
                this
            );
        }
    }

    private int CalculateScore()
    {
        int secondCompleted =
            Mathf.CeilToInt(elapsedTime);

        secondCompleted =
            Mathf.Clamp(
                secondCompleted,
                1,
                10
            );

        return 1100 -
               (secondCompleted * 100);
    }

    private void UpdateTimerUI()
    {
        if (timerText == null)
        {
            return;
        }

        timerText.text =
            "Time: " +
            Mathf.CeilToInt(timeRemaining);
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
            return;
        }

        pauseButton.gameObject.SetActive(true);
        pauseButton.interactable = true;
    }
}