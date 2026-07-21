using UnityEngine;

public class GameManagerSTS : MonoBehaviour
{
    [Header("Food Frenzy UI Infrastructure")]
    [SerializeField] private MiniGameTimerScore miniGameTimerScore;

    [Header("Shoot the Sauce")]
    [SerializeField] private PizzaController pizzaController;

    private bool gameCompleted;

    public bool GameCompleted
    {
        get
        {
            return gameCompleted;
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

        if (pizzaController != null)
        {
            pizzaController.FinishGame();
        }

        if (miniGameTimerScore == null)
        {
            Debug.LogError(
                "MiniGameTimerScore has not been assigned to GameManagerSTS.",
                this
            );

            return;
        }

        /*
         * This keeps Shoot the Sauce compatible with the
         * existing Food Frenzy score and transition system.
         */
        miniGameTimerScore.CompleteTask();
    }
}