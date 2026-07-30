using UnityEngine;

public class CherryMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CherryState cherryState;
    [SerializeField] private Rigidbody2D cherryRigidbody;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;

    [Header("Movement")]
    [SerializeField] private float horizontalSpeed = 4f;
    [SerializeField] private float fallingSpeed = 7f;
    [SerializeField] private bool startMovingRight = true;

    private int horizontalDirection;

    private void Awake()
    {
        FindMissingReferences();
    }

    private void Start()
    {
        ResetDirection();
        MoveToSpawnPoint();
    }

    private void FixedUpdate()
    {
        if (cherryState == null)
        {
            return;
        }

        if (cherryState.CurrentState == CherryState.State.MovingHorizontally)
        {
            MoveHorizontally();
        }
        else if (cherryState.CurrentState == CherryState.State.Falling)
        {
            MoveDownward();
        }
    }

    private void FindMissingReferences()
    {
        if (cherryState == null)
        {
            cherryState = GetComponent<CherryState>();
        }

        if (cherryRigidbody == null)
        {
            cherryRigidbody = GetComponent<Rigidbody2D>();
        }
    }

    public void BeginHorizontalMovement()
    {
        if (cherryState == null)
        {
            return;
        }

        if (cherryState.CurrentState != CherryState.State.Waiting &&
            cherryState.CurrentState != CherryState.State.Respawning)
        {
            return;
        }

        ResetDirection();
        cherryState.SetState(CherryState.State.MovingHorizontally);
    }

    public void Drop()
    {
        if (cherryState == null || !cherryState.CanDrop)
        {
            return;
        }

        cherryState.SetState(CherryState.State.Falling);
    }

    private void MoveHorizontally()
    {
        if (!HasValidMovementReferences())
        {
            return;
        }

        Vector2 currentPosition = cherryRigidbody.position;

        float movement =
            horizontalDirection *
            horizontalSpeed *
            Time.fixedDeltaTime;

        Vector2 nextPosition = new Vector2(
            currentPosition.x + movement,
            currentPosition.y
        );

        if (nextPosition.x >= rightBoundary.position.x)
        {
            nextPosition.x = rightBoundary.position.x;
            horizontalDirection = -1;
        }
        else if (nextPosition.x <= leftBoundary.position.x)
        {
            nextPosition.x = leftBoundary.position.x;
            horizontalDirection = 1;
        }

        cherryRigidbody.MovePosition(nextPosition);
    }

    private void MoveDownward()
    {
        if (cherryRigidbody == null)
        {
            return;
        }

        Vector2 currentPosition = cherryRigidbody.position;

        Vector2 nextPosition = new Vector2(
            currentPosition.x,
            currentPosition.y - fallingSpeed * Time.fixedDeltaTime
        );

        cherryRigidbody.MovePosition(nextPosition);
    }

    public void MoveToSpawnPoint()
    {
        if (spawnPoint == null)
        {
            Debug.LogError(
                "CherryMovement requires a Spawn Point reference."
            );

            return;
        }

        if (cherryRigidbody != null)
        {
            cherryRigidbody.position = spawnPoint.position;
        }
        else
        {
            transform.position = spawnPoint.position;
        }
    }

    public void StopMovement()
    {
        if (cherryRigidbody == null)
        {
            return;
        }

#if UNITY_6000_0_OR_NEWER
        cherryRigidbody.linearVelocity = Vector2.zero;
#else
        cherryRigidbody.velocity = Vector2.zero;
#endif

        cherryRigidbody.angularVelocity = 0f;
    }

    public void ResetDirection()
    {
        horizontalDirection = startMovingRight ? 1 : -1;
    }

    private bool HasValidMovementReferences()
    {
        if (cherryRigidbody == null)
        {
            return false;
        }

        if (leftBoundary == null || rightBoundary == null)
        {
            return false;
        }

        return true;
    }
}