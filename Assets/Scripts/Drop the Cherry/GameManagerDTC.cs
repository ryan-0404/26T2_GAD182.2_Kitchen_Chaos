using UnityEngine;

public class GameManagerDTC : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private MiniGameTimerScore miniGameTimerScore;
    [SerializeField] private CherryMovement cherryMovement;

    private bool gameCompleted;

    public bool CanPlay
    {
        get
        {
            return !gameCompleted;
        }
    }

    public bool GameCompleted
    {
        get
        {
            return gameCompleted;
        }
    }

    private void Start()
    {
        gameCompleted = false;
    }

    public void CherryLandedOnCupcake()
    {
        if (gameCompleted)
        {
            return;
        }

        gameCompleted = true;

        if (cherryMovement != null)
        {
            cherryMovement.StopMovement();
        }

        if (miniGameTimerScore == null)
        {
            Debug.LogError(
                "MiniGameTimerScore has not been assigned to GameManagerDTC.",
                this
            );

            return;
        }

        miniGameTimerScore.CompleteTask();
    }
}