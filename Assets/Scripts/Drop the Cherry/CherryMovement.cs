using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class CherryMovement : MonoBehaviour
{
    private enum CherryState
    {
        Waiting,
        MovingSideways,
        Falling,
        Respawning,
        Finished
    }

    [Header("Required References")]
    [SerializeField] private GameManagerDTC gameManagerDTC;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;

    [Header("Movement")]
    [SerializeField] private float horizontalSpeed = 4f;
    [SerializeField] private float fallingSpeed = 7f;

    [Header("Respawn")]
    [SerializeField] private float respawnDelay = 0.25f;

    private Rigidbody2D cherryRigidbody;
    private Collider2D cherryCollider;
    private SpriteRenderer cherryRenderer;

    private CherryState currentState;
    private int horizontalDirection;

    public bool CanDrop
    {
        get
        {
            return currentState == CherryState.MovingSideways;
        }
    }

    public bool IsFalling
    {
        get
        {
            return currentState == CherryState.Falling;
        }
    }

    private void Awake()
    {
        cherryRigidbody = GetComponent<Rigidbody2D>();
        cherryCollider = GetComponent<Collider2D>();
        cherryRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        horizontalDirection = 1;

        ResetCherry();

        currentState = CherryState.Waiting;
    }

    private void FixedUpdate()
    {
        if (gameManagerDTC != null &&
            gameManagerDTC.GameCompleted)
        {
            return;
        }

        if (currentState == CherryState.MovingSideways)
        {
            MoveSideways();
        }
        else if (currentState == CherryState.Falling)
        {
            MoveDownward();
        }
    }

    public void BeginMovement()
    {
        if (currentState != CherryState.Waiting)
        {
            return;
        }

        currentState = CherryState.MovingSideways;
    }

    public void DropCherry()
    {
        if (!CanDrop)
        {
            return;
        }

        currentState = CherryState.Falling;
    }

    private void MoveSideways()
    {
        if (leftBoundary == null ||
            rightBoundary == null)
        {
            return;
        }

        Vector2 position = cherryRigidbody.position;

        position.x +=
            horizontalDirection *
            horizontalSpeed *
            Time.fixedDeltaTime;

        if (position.x >= rightBoundary.position.x)
        {
            position.x = rightBoundary.position.x;
            horizontalDirection = -1;
        }
        else if (position.x <= leftBoundary.position.x)
        {
            position.x = leftBoundary.position.x;
            horizontalDirection = 1;
        }

        cherryRigidbody.MovePosition(position);
    }

    private void MoveDownward()
    {
        Vector2 position = cherryRigidbody.position;

        position.y -=
            fallingSpeed *
            Time.fixedDeltaTime;

        cherryRigidbody.MovePosition(position);
    }

    public void LandOnCupcake()
    {
        if (!IsFalling)
        {
            return;
        }

        currentState = CherryState.Finished;

        StopMovement();

        if (cherryCollider != null)
        {
            cherryCollider.enabled = false;
        }

        if (gameManagerDTC != null)
        {
            gameManagerDTC.CherryLandedOnCupcake();
        }
    }

    public void CherryMissed()
    {
        if (!IsFalling)
        {
            return;
        }

        StartCoroutine(RespawnCherry());
    }

    private IEnumerator RespawnCherry()
    {
        currentState = CherryState.Respawning;

        StopMovement();
        SetCherryVisible(false);

        yield return new WaitForSeconds(respawnDelay);

        if (gameManagerDTC != null &&
            gameManagerDTC.GameCompleted)
        {
            yield break;
        }

        ResetCherry();
        SetCherryVisible(true);

        currentState = CherryState.MovingSideways;
    }

    private void ResetCherry()
    {
        if (spawnPoint == null)
        {
            Debug.LogError(
                "Cherry spawn point has not been assigned.",
                this
            );

            return;
        }

        cherryRigidbody.position = spawnPoint.position;
        cherryRigidbody.rotation = 0f;

        horizontalDirection = 1;

        StopMovement();
    }

    public void StopMovement()
    {
        if (cherryRigidbody == null)
        {
            return;
        }

        cherryRigidbody.linearVelocity = Vector2.zero;
        cherryRigidbody.angularVelocity = 0f;
    }

    private void SetCherryVisible(bool visible)
    {
        if (cherryRenderer != null)
        {
            cherryRenderer.enabled = visible;
        }

        if (cherryCollider != null)
        {
            cherryCollider.enabled = visible;
        }
    }
}