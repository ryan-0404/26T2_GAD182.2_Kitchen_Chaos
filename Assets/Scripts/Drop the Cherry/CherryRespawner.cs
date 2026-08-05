using System.Collections;
using UnityEngine;

public class CherryRespawn : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private CherryState cherryState;
    [SerializeField] private CherryMovement cherryMovement;

    [Header("Components")]
    [SerializeField] private SpriteRenderer cherryRenderer;
    [SerializeField] private Collider2D cherryCollider;

    [Header("Respawning")]
    [SerializeField] private float respawnDelay = 0.35f;

    private Coroutine respawnCoroutine;

    private void Awake()
    {
        if (cherryRenderer == null)
        {
            cherryRenderer = GetComponent<SpriteRenderer>();
        }

        if (cherryCollider == null)
        {
            cherryCollider = GetComponent<Collider2D>();
        }
    }

    public void BeginRespawn()
    {
        if (cherryState == null)
        {
            return;
        }

        if (!cherryState.IsFalling)
        {
            return;
        }

        if (respawnCoroutine != null)
        {
            return;
        }

        respawnCoroutine = StartCoroutine(
            RespawnRoutine()
        );
    }

    private IEnumerator RespawnRoutine()
    {
        cherryState.SetPhase(CherryPhase.Respawning);

        if (cherryMovement != null)
        {
            cherryMovement.StopRigidbodyMovement();
        }

        SetCherryVisible(false);

        yield return new WaitForSeconds(respawnDelay);

        if (cherryState.IsFinished)
        {
            respawnCoroutine = null;
            yield break;
        }

        if (cherryMovement != null)
        {
            cherryMovement.ResetToSpawn();
        }

        SetCherryVisible(true);

        cherryState.SetPhase(CherryPhase.Waiting);

        if (cherryMovement != null)
        {
            cherryMovement.BeginHorizontalMovement();
        }

        respawnCoroutine = null;
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