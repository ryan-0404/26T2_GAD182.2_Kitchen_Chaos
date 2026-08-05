using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CherryMovement : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private CherryState cherryState;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;

    [Header("Movement")]
    [SerializeField] private float horizontalSpeed = 4f;
    [SerializeField] private bool beginMovingRight = true;

    private Rigidbody2D cherryRigidbody;
    private int horizontalDirection;

    private void Awake()
    {
        cherryRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetStartingDirection();
        ResetToSpawn();

        if (cherryState != null)
        {
            cherryState.SetPhase(CherryPhase.Waiting);
        }
    }

    private void FixedUpdate()
    {
        if (cherryState == null)
        {
            return;
        }

        if (!cherryState.CanMoveHorizontally)
        {
            return;
        }

        MoveHorizontally();
    }

    public void BeginHorizontalMovement()
    {
        if (cherryState == null)
        {
            return;
        }

        if (cherryState.CurrentPhase != CherryPhase.Waiting &&
            cherryState.CurrentPhase != CherryPhase.Respawning)
        {
            return;
        }

        SetStartingDirection();

        cherryState.SetPhase(
            CherryPhase.MovingHorizontally
        );
    }

    private void MoveHorizontally()
    {
        if (cherryRigidbody == null ||
            leftBoundary == null ||
            rightBoundary == null)
        {
            return;
        }

        Vector2 nextPosition = cherryRigidbody.position;

        nextPosition.x +=
            horizontalDirection *
            horizontalSpeed *
            Time.fixedDeltaTime;

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

    public void ResetToSpawn()
    {
        if (spawnPoint == null)
        {
            Debug.LogError(
                "CherryMovement requires a Spawn Point reference.",
                this
            );

            return;
        }

        if (cherryRigidbody != null)
        {
            cherryRigidbody.position = spawnPoint.position;
            StopRigidbodyMovement();
        }
        else
        {
            transform.position = spawnPoint.position;
        }
    }

    public void StopRigidbodyMovement()
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

    private void SetStartingDirection()
    {
        if (beginMovingRight)
        {
            horizontalDirection = 1;
        }
        else
        {
            horizontalDirection = -1;
        }
    }
}