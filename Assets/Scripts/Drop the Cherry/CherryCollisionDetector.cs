using UnityEngine;

public class CherryCollisionDetector : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private CherryState cherryState;
    [SerializeField] private CherryDrop cherryDrop;
    [SerializeField] private CherryRespawn cherryRespawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (cherryState == null)
        {
            return;
        }

        if (!cherryState.IsFalling)
        {
            return;
        }

        CherryTargetZone targetZone =
            other.GetComponent<CherryTargetZone>();

        if (targetZone == null)
        {
            targetZone =
                other.GetComponentInParent<CherryTargetZone>();
        }

        if (targetZone == null)
        {
            return;
        }

        if (targetZone.IsMissZone)
        {
            if (cherryRespawn != null)
            {
                cherryRespawn.BeginRespawn();
            }

            return;
        }

        if (cherryDrop != null)
        {
            cherryDrop.LandOnCupcake(
                targetZone.ScoreMultiplier
            );
        }
    }
}