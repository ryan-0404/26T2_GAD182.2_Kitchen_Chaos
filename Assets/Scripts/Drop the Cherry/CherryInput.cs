using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CherryInput : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private CherryMovement cherryMovement;
    [SerializeField] private GameManagerDTC gameManagerDTC;
    [SerializeField] private MiniGameTimerScore miniGameTimerScore;

    [Header("Input Settings")]
    [SerializeField] private float inputDelay = 2f;

    private bool delayFinished;
    private bool gameplayActivated;

    private void Start()
    {
        delayFinished = false;
        gameplayActivated = false;

        StartCoroutine(EnableInputAfterDelay());
    }

    private void Update()
    {
        ActivateGameplayWhenReady();

        if (!gameplayActivated)
        {
            return;
        }

        if (!delayFinished)
        {
            return;
        }

        if (cherryMovement == null)
        {
            return;
        }

        if (gameManagerDTC == null)
        {
            return;
        }

        if (!gameManagerDTC.CanPlay)
        {
            return;
        }

        if (!cherryMovement.CanDrop)
        {
            return;
        }

        CheckForInput();
    }

    private IEnumerator EnableInputAfterDelay()
    {
        yield return new WaitForSeconds(inputDelay);

        delayFinished = true;
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
            cherryMovement.BeginMovement();
        }
    }

    private void CheckForInput()
    {
        bool spacePressed =
            Input.GetKeyDown(KeyCode.Space);

        bool mousePressed =
            Input.GetMouseButtonDown(0);

        if (!spacePressed && !mousePressed)
        {
            return;
        }

        if (mousePressed &&
            EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        cherryMovement.DropCherry();
    }
}