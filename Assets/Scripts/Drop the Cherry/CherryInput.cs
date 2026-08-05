using UnityEngine;
using UnityEngine.EventSystems;

public class CherryInput : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private CherryState cherryState;
    [SerializeField] private CherryMovement cherryMovement;
    [SerializeField] private CherryDrop cherryDrop;
    [SerializeField] private MiniGameTimerScore miniGameTimerScore;

    [Header("Input Options")]
    [SerializeField] private bool allowSpaceKey = true;
    [SerializeField] private bool allowLeftMouseButton = true;

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

        if (cherryState == null ||
            cherryDrop == null)
        {
            return;
        }

        if (!cherryState.CanDrop)
        {
            return;
        }

        if (DropInputPressed())
        {
            cherryDrop.DropCherry();
        }
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

        if (cherryMovement != null)
        {
            cherryMovement.BeginHorizontalMovement();
        }
    }

    private bool DropInputPressed()
    {
        if (allowSpaceKey &&
            Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        if (allowLeftMouseButton &&
            Input.GetMouseButtonDown(0))
        {
            if (PointerIsOverUI())
            {
                return false;
            }

            return true;
        }

        return false;
    }

    private bool PointerIsOverUI()
    {
        if (EventSystem.current == null)
        {
            return false;
        }

        return EventSystem.current.IsPointerOverGameObject();
    }
}