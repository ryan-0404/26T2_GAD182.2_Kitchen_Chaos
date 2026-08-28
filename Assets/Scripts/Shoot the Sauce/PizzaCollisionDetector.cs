using UnityEngine;

public class PizzaCollisionDetector : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private PizzaMovement pizzaMovement;
    [SerializeField] private GameManagerSTS gameManagerSTS;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pizzaMovement == null)
        {
            return;
        }

        if (gameManagerSTS == null)
        {
            return;
        }

        if (gameManagerSTS.GameCompleted)
        {
            return;
        }

        if (!pizzaMovement.HasBeenShot)
        {
            return;
        }

        PizzaTargetZone targetZone =
            other.GetComponent<PizzaTargetZone>();

        if (targetZone == null)
        {
            targetZone =
                other.GetComponentInParent<PizzaTargetZone>();
        }

        if (targetZone == null)
        {
            return;
        }

        if (targetZone.IsMissZone)
        {
            gameManagerSTS.PizzaMissed();
            return;
        }

        if (pizzaMovement.HasLandedSuccessfully)
        {
            return;
        }

        gameManagerSTS.PizzaLandedSuccessfully();
    }
}