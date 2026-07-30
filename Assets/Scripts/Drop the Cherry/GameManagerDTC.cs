using UnityEngine;

public class GameManagerDTC : MonoBehaviour
{
    [Header("Existing Systems")]
    [SerializeField] private MiniGameTimerScore miniGameTimerScore;

    [Header("Debug")]
    [SerializeField] private bool miniGameCompleted;

    private void Start()
    {
        miniGameCompleted = false;
    }

    public void CompleteMiniGame(float scoreMultiplier)
    {
        if (miniGameCompleted)
        {
            return;
        }

        if (miniGameTimerScore == null)
        {
            Debug.LogError(
                "GameManagerDTC requires a MiniGameTimerScore reference."
            );

            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogError(
                "GameManagerDTC could not find the persistent GameManager."
            );

            return;
        }

        miniGameCompleted = true;

        float validMultiplier = Mathf.Clamp01(scoreMultiplier);

        int timeScore =
            miniGameTimerScore.GetCurrentTimeScore();

        int finalScore =
            Mathf.RoundToInt(timeScore * validMultiplier);

        miniGameTimerScore.StopTimerForExternalCompletion();

        GameManager.Instance.CompleteMiniGame(
            finalScore,
            true
        );
    }
}