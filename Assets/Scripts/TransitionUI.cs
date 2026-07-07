using UnityEngine;
using TMPro;
using System.Collections;

public class TransitionUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text totalScoreText;
    [SerializeField] private CookieLivesUI cookieLivesUI;

    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 5f;

    private Coroutine transitionCoroutine;
    private float remainingTransitionTime;
    private bool transitionPaused;

    private void Start()
    {
        Time.timeScale = 1f;

        remainingTransitionTime = transitionDuration;
        transitionPaused = false;

        if (totalScoreText != null && GameManager.Instance != null)
        {
            totalScoreText.text =
                "Total Score: " + GameManager.Instance.totalScore;
        }

        if (cookieLivesUI != null)
        {
            cookieLivesUI.RefreshLives();
        }

        transitionCoroutine =
            StartCoroutine(LoadNextGameAfterDelay());
    }

    private IEnumerator LoadNextGameAfterDelay()
    {
        while (remainingTransitionTime > 0f)
        {
            if (!transitionPaused)
            {
                remainingTransitionTime -= Time.unscaledDeltaTime;
            }

            yield return null;
        }

        transitionCoroutine = null;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadNextMiniGame();
        }
    }

    public void PauseTransition()
    {
        transitionPaused = true;
    }

    public void ResumeTransition()
    {
        transitionPaused = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReturnToMainMenu();
        }
    }
}