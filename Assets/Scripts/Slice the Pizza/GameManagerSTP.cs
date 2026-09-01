using System.Collections;
using UnityEngine;

public class GameManagerSTP : MonoBehaviour
{
    [Header("Food Frenzy Infrastructure")]
    [SerializeField]
    private MiniGameTimerScore miniGameTimerScore;

    [Header("Slice the Pizza Systems")]
    [SerializeField]
    private PizzaSliceMarker sliceMarker;

    [SerializeField]
    private SliceThePizzaTarget target;

    [SerializeField]
    private SliceThePizzaJudge judge;

    [SerializeField]
    private SliceThePizzaFeedback feedback;

    [Header("Completion")]
    [SerializeField]
    private float successDisplayTime = 0.75f;

    private bool gameStarted;
    private bool gameFinished;
    private bool inputLocked;

    public bool CanAcceptInput
    {
        get
        {
            if (!gameStarted)
            {
                return false;
            }

            if (gameFinished)
            {
                return false;
            }

            if (inputLocked)
            {
                return false;
            }

            return true;
        }
    }

    private void Start()
    {
        gameStarted = false;
        gameFinished = false;
        inputLocked = false;

        if (sliceMarker != null)
        {
            sliceMarker.ResetMarker();
        }
    }

    private void Update()
    {
        if (gameFinished)
        {
            return;
        }

        if (miniGameTimerScore == null)
        {
            return;
        }

        if (miniGameTimerScore.GameEnded)
        {
            StopGame();
            return;
        }

        if (!gameStarted &&
            miniGameTimerScore.GameplayStarted)
        {
            StartGame();
        }

        if (feedback != null &&
            feedback.FeedbackPlaying)
        {
            inputLocked = true;
        }
        else if (!gameFinished)
        {
            inputLocked = false;
        }
    }

    private void StartGame()
    {
        gameStarted = true;

        if (target != null)
        {
            target.ShowTarget();
        }

        if (sliceMarker != null)
        {
            sliceMarker.StartMoving();
        }
    }

    public void PlayerPressedSpace()
    {
        if (!CanAcceptInput)
        {
            return;
        }

        if (sliceMarker == null ||
            target == null ||
            judge == null)
        {
            return;
        }

        SliceResult result =
            judge.JudgeAttempt(
                sliceMarker.CurrentPercentage,
                target.TargetPercentage
            );

        if (result == SliceResult.Perfect)
        {
            PerfectAttempt();
            return;
        }

        if (result == SliceResult.Good)
        {
            GoodAttempt();
            return;
        }

        RetryAttempt();
    }

    private void PerfectAttempt()
    {
        gameFinished = true;
        inputLocked = true;

        sliceMarker.StopMoving();

        if (feedback != null)
        {
            feedback.ShowPerfect();
        }

        StartCoroutine(
            CompleteAfterFeedback()
        );
    }

    private void GoodAttempt()
    {
        gameFinished = true;
        inputLocked = true;

        sliceMarker.StopMoving();

        if (feedback != null)
        {
            feedback.ShowGood();
        }

        StartCoroutine(
            CompleteAfterFeedback()
        );
    }

    private void RetryAttempt()
    {
        inputLocked = true;

        sliceMarker.StopMoving();

        if (feedback != null)
        {
            feedback.FlashKnife();
        }

        StartCoroutine(
            ResumeAfterFlash()
        );
    }

    private IEnumerator ResumeAfterFlash()
    {
        while (feedback != null &&
               feedback.FeedbackPlaying)
        {
            yield return null;
        }

        if (gameFinished)
        {
            yield break;
        }

        if (miniGameTimerScore != null &&
            miniGameTimerScore.GameEnded)
        {
            yield break;
        }

        sliceMarker.StartMoving();

        inputLocked = false;
    }

    private IEnumerator CompleteAfterFeedback()
    {
        yield return new WaitForSeconds(
            successDisplayTime
        );

        if (miniGameTimerScore != null)
        {
            miniGameTimerScore.CompleteTask();
        }
    }

    private void StopGame()
    {
        gameStarted = false;
        gameFinished = true;
        inputLocked = true;

        if (sliceMarker != null)
        {
            sliceMarker.StopMoving();
        }
    }
}