using UnityEngine;

public class GameManagerSTS : MonoBehaviour
{
    [Header("Food Frenzy UI Infrastructure")]
    [SerializeField] private MiniGameTimerScore miniGameTimerScore;

    [Header("Shoot the Sauce")]
    [SerializeField] private PizzaMovement pizzaMovement;

    private bool gameCompleted;

    public bool GameCompleted
    {
        get
        {
            return gameCompleted;
        }
    }

    public bool CanPlay
    {
        get
        {
            if (gameCompleted)
            {
                return false;
            }

            if (miniGameTimerScore == null)
            {
                return false;
            }

            return miniGameTimerScore.GameplayStarted;
        }
    }

    private void Awake()
    {
        gameCompleted = false;
    }

    public void CompleteMiniGame()
    {
        if (gameCompleted)
        {
            return;
        }

        gameCompleted = true;

        if (pizzaMovement != null)
        {
            pizzaMovement.StopMovement();
        }

        if (miniGameTimerScore == null)
        {
            Debug.LogError(
                "MiniGameTimerScore has not been assigned to GameManagerSTS.",
                this
            );

            return;
        }

        miniGameTimerScore.CompleteTask();
    }

    public void PizzaMissed()
    {
        if (gameCompleted)
        {
            return;
        }

        if (pizzaMovement == null)
        {
            Debug.LogError(
                "PizzaMovement has not been assigned to GameManagerSTS.",
                this
            );

            return;
        }

        pizzaMovement.ResetPizza();
    }
}