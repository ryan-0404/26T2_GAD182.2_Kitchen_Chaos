using System.Collections;
using UnityEngine;

public class GameManagerSTS : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private MiniGameTimerScore miniGameTimerScore;
    [SerializeField] private PizzaMovement pizzaMovement;

    [Header("Respawn Settings")]
    [SerializeField] private float respawnDelay = 0.15f;

    private bool isRespawning;
    private bool gameCompleted;

    public bool CanPlay => !isRespawning && !gameCompleted;

    public void PizzaLandedOnConveyor()
    {
        if (!CanPlay || pizzaMovement == null)
        {
            return;
        }

        if (!pizzaMovement.HasBeenShot)
        {
            return;
        }

        gameCompleted = true;
        pizzaMovement.StopMovement();

        if (miniGameTimerScore == null)
        {
            Debug.LogError(
                "MiniGameTimerScore has not been assigned.",
                this
            );

            return;
        }

        miniGameTimerScore.CompleteTask();
    }

    public void PizzaMissed()
    {
        if (!CanPlay || pizzaMovement == null)
        {
            return;
        }

        if (!pizzaMovement.HasBeenShot)
        {
            return;
        }

        StartCoroutine(RespawnPizza());
    }

    private IEnumerator RespawnPizza()
    {
        isRespawning = true;

        pizzaMovement.StopMovement();
        pizzaMovement.HidePizza();

        yield return new WaitForSeconds(respawnDelay);

        pizzaMovement.ResetPizza();
        pizzaMovement.ShowPizza();

        isRespawning = false;
    }
}