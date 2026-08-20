using UnityEngine;

public class CherryCollisionDetector : MonoBehaviour
{
    [Header("Required Reference")]
    [SerializeField] private CherryMovement cherryMovement;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Cherry collided with: " + other.gameObject.name);

        if (cherryMovement == null)
        {
            return;
        }

        if (!cherryMovement.IsFalling)
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
            cherryMovement.CherryMissed();
            return;
        }

        cherryMovement.LandOnCupcake();
    }
}