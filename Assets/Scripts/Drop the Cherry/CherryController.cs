using UnityEngine;

public class CherryController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CherryState cherryState;
    [SerializeField] private CherryMovement cherryMovement;
    [SerializeField] private CherryRespawner cherryRespawner;
    [SerializeField] private GameManagerDTC gameManagerDTC;
    [SerializeField] private Collider2D cherryCollider;

    private void Awake()
    {
        FindMissingReferences();
    }

    private void FindMissingReferences()
    {
        if (cherryState == null)
        {
            cherryState = GetComponent<CherryState>();
        }

        if (cherryMovement == null)
        {
            cherryMovement = GetComponent<CherryMovement>();
        }

        if (cherryRespawner == null)
        {
            cherryRespawner = GetComponent<CherryRespawner>();
        }

        if (cherryCollider == null)
        {
            cherryCollider = GetComponent<Collider2D>();
        }
    }

    public void BeginGameplay()
    {
        if (cherryMovement == null)
        {
            return;
        }

        cherryMovement.BeginHorizontalMovement();
    }

    public void DropCherry()
    {
        if (cherryMovement == null)
        {
            return;
        }

        cherryMovement.Drop();
    }

    public void LandOnCupcake(float scoreMultiplier)
    {
        if (cherryState == null || !cherryState.IsFalling)
        {
            return;
        }

        cherryState.SetState(CherryState.State.Finished);

        if (cherryMovement != null)
        {
            cherryMovement.StopMovement();
        }

        if (cherryCollider != null)
        {
            cherryCollider.enabled = false;
        }

        if (gameManagerDTC != null)
        {
            gameManagerDTC.CompleteMiniGame(scoreMultiplier);
        }
        else
        {
            Debug.LogError(
                "CherryController requires a GameManagerDTC reference."
            );
        }
    }

    public void MissCupcake()
    {
        if (cherryState == null || !cherryState.IsFalling)
        {
            return;
        }

        if (cherryRespawner != null)
        {
            cherryRespawner.Respawn();
        }
        else
        {
            Debug.LogError(
                "CherryController requires a CherryRespawner reference."
            );
        }
    }
}