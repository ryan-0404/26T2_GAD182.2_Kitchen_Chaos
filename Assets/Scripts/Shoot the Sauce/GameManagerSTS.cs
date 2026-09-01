using UnityEngine;

public class GameManagerSTS : MonoBehaviour
{
    [Header("Food Frenzy UI Infrastructure")]
    [SerializeField] private MiniGameTimerScore miniGameTimerScore;

    [Header("Shoot the Sauce")]
    [SerializeField] private PizzaMovement pizzaMovement;
    [SerializeField] private ShootTargetSelector shootTargetSelector;

    [Header("Win Condition")]
    [SerializeField] private int pizzasRequired = 3;

    private int successfulPizzas;
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
        successfulPizzas = 0;
        gameCompleted = false;
    }

    public void PizzaLandedSuccessfully()
    {
        if (gameCompleted)
        {
            return;
        }

        if (pizzaMovement == null)
        {
            return;
        }

        pizzaMovement.MarkSuccessfulLanding();
    }

    public void PizzaExitedAfterSuccess()
    {
        if (gameCompleted)
        {
            return;
        }

        successfulPizzas++;

        if (successfulPizzas >= pizzasRequired)
        {
            CompleteMiniGame();
            return;
        }

        if (shootTargetSelector != null)
        {
            shootTargetSelector.SelectTargetLane();
        }

        if (pizzaMovement != null)
        {
            pizzaMovement.ResetPizza();
        }
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


}