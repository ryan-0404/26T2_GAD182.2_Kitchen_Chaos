using UnityEngine;

public class SliceThePizzaJudge : MonoBehaviour
{
    [Header("Accuracy Settings")]
    [SerializeField]
    private float perfectTolerance = 0.15f;

    [SerializeField]
    private float goodTolerance = 1f;

    public SliceResult JudgeAttempt(
        float currentPercentage,
        float targetPercentage
    )
    {
        float difference =
            Mathf.Abs(
                currentPercentage -
                targetPercentage
            );

        difference =
            Mathf.Min(
                difference,
                100f - difference
            );

        if (difference <= perfectTolerance)
        {
            return SliceResult.Perfect;
        }

        if (difference <= goodTolerance)
        {
            return SliceResult.Good;
        }

        return SliceResult.Retry;
    }
}

public enum SliceResult
{
    Perfect,
    Good,
    Retry
}