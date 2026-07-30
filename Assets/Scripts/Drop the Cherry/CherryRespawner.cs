using System.Collections;
using UnityEngine;

public class CherryRespawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CherryState cherryState;
    [SerializeField] private CherryMovement cherryMovement;
    [SerializeField] private SpriteRenderer cherrySpriteRenderer;
    [SerializeField] private Collider2D cherryCollider;

    [Header("Respawning")]
    [SerializeField] private float respawnDelay = 0.35f;

    private Coroutine respawnCoroutine;

    public bool IsRespawning => respawnCoroutine != null;

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

        if (cherrySpriteRenderer == null)
        {
            cherrySpriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (cherryCollider == null)
        {
            cherryCollider = GetComponent<Collider2D>();
        }
    }

    public void Respawn()
    {
        if (respawnCoroutine != null)
        {
            return;
        }

        respawnCoroutine = StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        if (cherryState != null)
        {
            cherryState.SetState(CherryState.State.Respawning);
        }

        if (cherryMovement != null)
        {
            cherryMovement.StopMovement();
        }

        SetCherryVisible(false);

        yield return new WaitForSeconds(respawnDelay);

        if (cherryMovement != null)
        {
            cherryMovement.MoveToSpawnPoint();
            cherryMovement.ResetDirection();
        }

        SetCherryVisible(true);

        if (cherryMovement != null)
        {
            cherryMovement.BeginHorizontalMovement();
        }

        respawnCoroutine = null;
    }

    private void SetCherryVisible(bool isVisible)
    {
        if (cherrySpriteRenderer != null)
        {
            cherrySpriteRenderer.enabled = isVisible;
        }

        if (cherryCollider != null)
        {
            cherryCollider.enabled = isVisible;
        }
    }
}