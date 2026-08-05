using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CherryDrop : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private CherryState cherryState;
    [SerializeField] private CherryMovement cherryMovement;
    [SerializeField] private GameManagerDTC gameManagerDTC;

    [Header("Components")]
    [SerializeField] private Collider2D cherryCollider;

    [Header("Falling")]
    [SerializeField] private float fallingSpeed = 7f;

    private Rigidbody2D cherryRigidbody;

    private void Awake()
    {
        cherryRigidbody = GetComponent<Rigidbody2D>();

        if (cherryCollider == null)
        {
            cherryCollider = GetComponent<Collider2D>();
        }
    }

    private void FixedUpdate()
    {
        if (cherryState == null)
        {
            return;
        }

        if (!cherryState.IsFalling)
        {
            return;
        }

        MoveDownward();
    }

    public void DropCherry()
    {
        if (cherryState == null)
        {
            return;
        }

        if (!cherryState.CanDrop)
        {
            return;
        }

        cherryState.SetPhase(CherryPhase.Falling);
    }

    private void MoveDownward()
    {
        if (cherryRigidbody == null)
        {
            return;
        }

        Vector2 nextPosition = cherryRigidbody.position;

        nextPosition.y -=
            fallingSpeed *
            Time.fixedDeltaTime;

        cherryRigidbody.MovePosition(nextPosition);
    }

    public void LandOnCupcake(float scoreMultiplier)
    {
        if (cherryState == null)
        {
            return;
        }

        if (!cherryState.IsFalling)
        {
            return;
        }

        cherryState.SetPhase(CherryPhase.Finished);

        if (cherryMovement != null)
        {
            cherryMovement.StopRigidbodyMovement();
        }

        if (cherryCollider != null)
        {
            cherryCollider.enabled = false;
        }

        if (gameManagerDTC == null)
        {
            Debug.LogError(
                "CherryDrop requires a GameManagerDTC reference.",
                this
            );

            return;
        }

        gameManagerDTC.CompleteMiniGame(
            scoreMultiplier
        );
    }
}