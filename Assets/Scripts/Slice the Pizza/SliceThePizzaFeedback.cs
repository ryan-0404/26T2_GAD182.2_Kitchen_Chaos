using System.Collections;
using TMPro;
using UnityEngine;

public class SliceThePizzaFeedback : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text resultText;

    [Header("Effects")]
    [SerializeField]
    private ParticleSystem confettiParticles;

    [Header("Knife Flash")]
    [SerializeField]
    private SpriteRenderer knifeRenderer;

    [SerializeField]
    private float flashDuration = 0.1f;

    private bool feedbackPlaying;

    public bool FeedbackPlaying
    {
        get
        {
            return feedbackPlaying;
        }
    }

    private void Start()
    {
        feedbackPlaying = false;

        if (resultText != null)
        {
            resultText.gameObject.SetActive(false);
        }
    }

    public void ShowPerfect()
    {
        ShowSuccessFeedback(
            "PERFECT!"
        );
    }

    public void ShowGood()
    {
        ShowSuccessFeedback(
            "GOOD!"
        );
    }

    private void ShowSuccessFeedback(
        string message
    )
    {
        if (resultText != null)
        {
            resultText.text = message;
            resultText.gameObject.SetActive(true);
        }

        if (confettiParticles != null)
        {
            confettiParticles.Play();
        }
    }

    public void FlashKnife()
    {
        if (feedbackPlaying)
        {
            return;
        }

        StartCoroutine(
            FlashKnifeRoutine()
        );
    }

    private IEnumerator FlashKnifeRoutine()
    {
        feedbackPlaying = true;

        for (int i = 0; i < 2; i++)
        {
            if (knifeRenderer != null)
            {
                knifeRenderer.enabled = false;
            }

            yield return new WaitForSeconds(
                flashDuration
            );

            if (knifeRenderer != null)
            {
                knifeRenderer.enabled = true;
            }

            yield return new WaitForSeconds(
                flashDuration
            );
        }

        feedbackPlaying = false;
    }
}