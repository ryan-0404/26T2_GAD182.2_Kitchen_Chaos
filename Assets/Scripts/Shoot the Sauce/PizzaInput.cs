using UnityEngine;
using UnityEngine.InputSystem;

public class ShootInput : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private PizzaMovement pizzaMovement;
    [SerializeField] private GameManagerSTS gameManagerSTS;
    [SerializeField] private MiniGameTimerScore miniGameTimerScore;

    private bool gameplayActivated;

    private void Start()
    {
        gameplayActivated = false;
    }

    private void Update()
    {
        ActivateGameplayWhenReady();

        if (!gameplayActivated)
        {
            return;
        }

        if (pizzaMovement == null ||
            gameManagerSTS == null)
        {
            return;
        }

        if (!gameManagerSTS.CanPlay)
        {
            return;
        }

        ReadHorizontalInput();
        ReadLaunchInput();
    }

    private void ActivateGameplayWhenReady()
    {
        if (gameplayActivated)
        {
            return;
        }

        if (miniGameTimerScore == null)
        {
            return;
        }

        if (!miniGameTimerScore.GameplayStarted)
        {
            return;
        }

        gameplayActivated = true;
    }

    private void ReadHorizontalInput()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        bool leftPressed =
            Keyboard.current.leftArrowKey.isPressed;

        bool rightPressed =
            Keyboard.current.rightArrowKey.isPressed;

        pizzaMovement.SetHorizontalInput(
            leftPressed,
            rightPressed
        );
    }

    private void ReadLaunchInput()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (!Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            return;
        }

        pizzaMovement.Launch();
    }
}